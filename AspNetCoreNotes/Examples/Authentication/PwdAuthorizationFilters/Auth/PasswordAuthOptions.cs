using System;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;

namespace Authentication.Auth
{
    public class PasswordAuthOptions : AuthenticationSchemeOptions
    {
        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(SignInScheme))
            {
                throw new ArgumentNullException(nameof(SignInScheme));
            }

            if (Endpoint == null)
            {
                throw new ArgumentNullException(nameof(Endpoint));
            }

            if (Client == null)
            {
                throw new ArgumentNullException(nameof(Client));
            }
        }

        public string SignInScheme { get; set; } = "Bearer";

        public Uri? Endpoint { get; set; }

        public HttpClient Client { get; set; } = new();
    }
}