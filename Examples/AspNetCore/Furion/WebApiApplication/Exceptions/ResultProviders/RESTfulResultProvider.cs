using System.Collections.Generic;
using System.Net;
using System.Text.Json;
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
    /// RESTful 风格返回值
    /// </summary>
    [SkipScan, UnifyModel(typeof(RESTfulResult<>))]
    public class RESTfulResultProvider : IUnifyResultProvider
    {
        public IActionResult OnException(ExceptionContext context)
        {
            return context.Exception switch
            {
                // 处理权限类异常
                PermissionDeniedException permissionDenied => HandleException(context, HttpStatusCode.Forbidden, new
                {
                    Code = permissionDenied.Key
                }),

                // 处理其他已知类异常
                AppException appException => HandleException(context, HttpStatusCode.BadRequest, new
                {
                    Code = appException.Key
                }),

                // 处理未知类异常
                _ => HandleException(context, HttpStatusCode.InternalServerError)
            };
        }

        private static IActionResult HandleException(ExceptionContext context, HttpStatusCode statusCode = HttpStatusCode.BadRequest, object extras = null)
        {
            return new JsonResult(new RESTfulResult<object>
            {
                StatusCode = (int)statusCode,
                Successed = false,
                Data = null,
                Errors = context.Exception.Message,
                Extras = extras
            })
            {
                StatusCode = (int)statusCode
            };
        }

        public IActionResult OnSuccessed(ActionExecutedContext context)
        {
            return context.Result;
        }

        public IActionResult OnValidateFailed(ActionExecutingContext context, ModelStateDictionary modelStates,
            Dictionary<string, IEnumerable<string>> validationResults, string validateFaildMessage)
        {
            return new JsonResult(new RESTfulResult<object>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Successed = false,
                Data = null,
                Errors = validationResults
            })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        public async Task OnResponseStatusCodes(HttpContext context, int statusCode)
        {
            switch (statusCode)
            {
                case StatusCodes.Status401Unauthorized:
                    await context.Response.WriteAsJsonAsync(new RESTfulResult<object>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Successed = false,
                        Data = null,
                        Errors = "401 Unauthorized",
                        Extras = UnifyResultContext.Take()
                    }, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    break;
                case StatusCodes.Status403Forbidden:
                    await context.Response.WriteAsJsonAsync(new RESTfulResult<object>
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Successed = false,
                        Data = null,
                        Errors = "403 Forbidden",
                        Extras = UnifyResultContext.Take()
                    }, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    break;
            }
        }
    }
}