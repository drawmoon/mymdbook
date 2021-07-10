using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MultiSchemeAuthentication.Middlewares
{
    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            List<Claim> claims = new()
            {
                new Claim("sub", Guid.NewGuid().ToString()),
                new Claim("userName", "admin"),
                new Claim("role", "管理员")
            };
            
            var principal = new ClaimsPrincipal();
            var claimsIdentity = new ClaimsIdentity(claims, "Identity.Application", "userName", "role");
            principal.AddIdentity(claimsIdentity);

            Context.User = principal;
            
            var result = AuthenticateResult.Success(new AuthenticationTicket(principal, "Identity.Application"));
            return Task.FromResult(result);
        }
    }
}