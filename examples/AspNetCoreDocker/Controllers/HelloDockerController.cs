using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloDocker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HelloDockerController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello Docker!";
        }
    }
}
