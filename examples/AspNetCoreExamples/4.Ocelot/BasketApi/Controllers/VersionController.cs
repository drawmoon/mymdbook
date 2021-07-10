using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    [ApiController]
    [Route("version")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetVersion()
        {
            return "1.0.0";
        }
    }
}