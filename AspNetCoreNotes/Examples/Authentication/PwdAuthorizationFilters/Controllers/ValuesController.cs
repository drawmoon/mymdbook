using Authentication.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET
        [PasswordAuth("Paw1")] // 使用 Paw1 认证模式
        [HttpGet("simple1")]
        public ActionResult<string[]> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET
        [PasswordAuth("Paw2")] // 使用 Paw2 认证模式
        [HttpGet("simple2")]
        public ActionResult<string[]> Get2()
        {
            return new[] { "value1", "value2" };
        }
    }
}