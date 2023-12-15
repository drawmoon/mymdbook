using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet("announcement")]
        public ActionResult<string> GetAnnouncement([FromServices] IConfiguration configuration)
        {
            return configuration["Announcement"];
        }
    }
}