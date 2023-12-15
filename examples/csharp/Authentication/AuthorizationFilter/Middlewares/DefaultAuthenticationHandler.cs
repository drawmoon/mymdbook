using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthorizationFilter.Middlewares
{
    public class DefaultAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public DefaultAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            List<Claim> claims = new()
            {
                new Claim("sub", Guid.NewGuid().ToString()),
                new Claim("userName", "admin"),
                new Claim("role", "Administrator")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Identity.Application", "userName", "role");
            var principal = new ClaimsPrincipal(claimsIdentity);

            Context.User = principal;

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, "Identity.Application")));
        }
    }
}