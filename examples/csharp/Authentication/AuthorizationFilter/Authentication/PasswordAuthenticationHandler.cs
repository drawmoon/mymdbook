using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AuthorizationFilter.Authentication
{
    public class PasswordAuthenticationHandler : AuthenticationHandler<PasswordAuthenticationOptions>
    {
        public PasswordAuthenticationHandler(
            IOptionsMonitor<PasswordAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // 在进行密码验证之前，先进行登录授权验证，确保用户的 Token 没有失效
            var authenticateResult = await Context.AuthenticateAsync(Options.SignInScheme);

            // 如果授权已经失败了，则直接返回授权结果
            if (authenticateResult.Failure != null)
            {
                return authenticateResult;
            }

            // 处理密码验证请求
            if (!await HandleRequestAsync())
                return AuthenticateResult.Fail("认证失败，密码无效");

            // 返回身份认证票据
            var ticket = authenticateResult.Ticket;
            return ticket?.Principal == null ? AuthenticateResult.Fail("Not authenticated") : AuthenticateResult.Success(new AuthenticationTicket(ticket.Principal, ticket.Properties, Scheme.Name));
        }

        private async Task<bool> HandleRequestAsync()
        {
            // 检查请求头中是否存在 X-Password
            if (!Context.Request.Headers.TryGetValue("X-Password", out var password) || StringValues.IsNullOrEmpty(password))
            {
                return false;
            }

            // 请求授权服务器，验证用户密码是否正确
            var responseMessage = await Options.Client.PostAsync(Options.Endpoint,
                new FormUrlEncodedContent(new[] { new KeyValuePair<string?, string?>("userPassword", password) }));

            // 返回请求的结果，是否成功
            return responseMessage.IsSuccessStatusCode;
        }
    }
}