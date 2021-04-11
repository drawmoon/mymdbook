using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authentication.Auth
{
    public class PasswordAuthHandlerSimple2 : PasswordAuthenticationHandler
    {
        public PasswordAuthHandlerSimple2(
            IOptionsMonitor<PasswordAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }
    }
}