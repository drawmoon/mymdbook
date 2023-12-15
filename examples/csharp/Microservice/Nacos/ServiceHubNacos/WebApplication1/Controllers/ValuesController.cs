using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string[]> Get()
        {
            return Ok(new[] { "value1", "value2" });
        }

        [HttpGet("announcement")]
        public ActionResult<string> GetAnnouncement([FromServices] IConfiguration configuration)
        {
            return configuration["Announcement"];
        }
    }
}
