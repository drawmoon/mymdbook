using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerExceptions.Exceptions;

namespace ControllerExceptions.Attributes
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
            ValidationProblemDetails problemDetails = new(context.ModelState)
            {
                Title = "发生错误。",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path
            };

            context.Result = new BadRequestObjectResult(problemDetails)
            {
                ContentTypes =
                {
                    "application/problem+json",
                    "application/problem+xml"
                }
            };
            context.ExceptionHandled = true;
        }

        private static void AddModelError(ExceptionContext context)
        {
            var appException = context.Exception as AppException;
            context.ModelState.AddModelError(appException!.Key.ToString(), appException.Message);
        }
    }
}
