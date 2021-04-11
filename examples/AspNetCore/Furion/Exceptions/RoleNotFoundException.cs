using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Exceptions
{
    public class RoleNotFoundException : AppException
    {
        public RoleNotFoundException(string message) : base(ErrorCodes.RoleNotFound, message)
        {
        }
    }
}
