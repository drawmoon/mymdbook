using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ControllerExceptionFilter.Exceptions
{
    public class AppException : Exception
    {
        public AppException(ErrorCodes errorCode, string message, int? statusCode = null) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;
        }

        public AppException(ErrorCodes errorCode, string message, Exception innerException, int? statusCode = null) : base(message, innerException)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode ?? StatusCodes.Status500InternalServerError;
        }

        public ErrorCodes ErrorCode { get; }

        public int StatusCode { get; private set; }

        public AppException SetStatusCode(int statusCode)
        {
            StatusCode = statusCode;
            return this;
        }
    }
}
