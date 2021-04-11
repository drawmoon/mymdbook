using System;

namespace WebApiApplication.Exceptions
{
    public class AppException : Exception
    {
        public ErrorCodes Key { get; }

        public AppException(ErrorCodes key, string message) : base(message)
        {
            Key = key;
        }
    }
}