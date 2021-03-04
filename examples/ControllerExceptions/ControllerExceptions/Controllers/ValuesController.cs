using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerExceptions.Attributes;
using ControllerExceptions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerExceptions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ControllerExceptionFilter]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string[]> Get()
        {
            throw new AppException(ErrorCodes.Param, "接收到的参数错误。");
        }
    }
}
