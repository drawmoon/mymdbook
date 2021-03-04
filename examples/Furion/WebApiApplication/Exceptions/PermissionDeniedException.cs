using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Exceptions
{
    public class PermissionDeniedException : AppException
    {
        public PermissionDeniedException(string message) : base(ErrorCodes.PermissionDenied, message)
        {
        }
    }
}
