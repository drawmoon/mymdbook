using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerExceptionFilter.Exceptions;

namespace ControllerExceptionFilter.Attributes
{
    public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is not AppException)
                return;

            AddModelError(context);
            HandleException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var appException = context.Exception as AppException;

            ValidationProblemDetails problemDetails = new(context.ModelState)
            {
                Title = "无法处理的请求",
                Status = appException!.StatusCode,
                Instance = context.HttpContext.Request.Path
            };

            context.Result = new ObjectResult(problemDetails)
            {
                ContentTypes =
                {
                    "application/problem+json",
                    "application/problem+xml"
                },
                StatusCode = appException.StatusCode
            };
            context.ExceptionHandled = true;
        }

        private static void AddModelError(ExceptionContext context)
        {
            var appException = context.Exception as AppException;
            context.ModelState.AddModelError(appException!.ErrorCode.ToString(), appException.Message);
        }
    }
}
