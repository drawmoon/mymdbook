using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationFilter.Authentication
{
    public class PasswordAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _scheme;

        public PasswordAuthorizationFilter(string scheme)
        {
            _scheme = scheme;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            OnAuthorizationAsync(context).GetAwaiter().GetResult();
        }

        private async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var provider = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            var handler = await provider.GetHandlerAsync(httpContext, _scheme);
            if (handler == null)
            {
                await HandleUnauthorizedAsync(context);
                return;
            }
            var authenticateResult = await handler.AuthenticateAsync();
            if (!authenticateResult.Succeeded)
                await HandleUnauthorizedAsync(context);
        }

        private static Task HandleUnauthorizedAsync(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedResult();
            return Task.CompletedTask;
        }
    }
}