using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControllerExceptions.Exceptions
{
    public class AppException : Exception
    {
        public ErrorCodes Key { get; }

        public AppException(ErrorCodes key, string message) : base(message)
        {
            Key = key;
        }

        public AppException(ErrorCodes key, string message, Exception innerException) : base(message, innerException)
        {
            Key = key;
        }
    }
}
