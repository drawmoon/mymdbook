using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApiApplication.Exceptions.ResultProviders
{
    /// <summary>
    /// Problem+json 风格返回值
    /// </summary>
    [SkipScan, UnifyModel(typeof(ValidationProblemDetails))]
    public class ProblemJsonResultProvider : IUnifyResultProvider
    {
        public IActionResult OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // 处理已知类异常
            if (exception is AppException appException)
            {
                context.ModelState.AddModelError(appException.Key.ToString(), appException.Message);
                return HandleException(context, exception switch
                {
                    PermissionDeniedException => HttpStatusCode.Forbidden,
                    _ => HttpStatusCode.BadRequest
                });
            }

            // 处理未知类异常
            context.ModelState.AddModelError("未知异常", exception.Message);
            return HandleException(context, HttpStatusCode.InternalServerError);
        }

        private static IActionResult HandleException(ActionContext context, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Title = "发生错误",
                Status = (int)status,
                Instance = context.HttpContext.Request.Path
            };

            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes =
                {
                    "application/problem+json",
                    "application/problem+xml"
                }
            };
        }

        public IActionResult OnSuccessed(ActionExecutedContext context)
        {
            return context.Result;
        }

        public IActionResult OnValidateFailed(ActionExecutingContext context, ModelStateDictionary modelStates,
            Dictionary<string, IEnumerable<string>> validationResults, string validateFaildMessage)
        {
            return HandleException(context);
        }

        public Task OnResponseStatusCodes(HttpContext context, int statusCode)
        {
            return Task.CompletedTask;
        }
    }
}