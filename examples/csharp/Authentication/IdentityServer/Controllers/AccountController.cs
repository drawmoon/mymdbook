using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/account/password/verify")]
        public IActionResult VerifyPassword([FromForm] string userPassword)
        {
            if (!string.IsNullOrWhiteSpace(userPassword) && userPassword == "admin")
            {
                return NoContent();
            }

            return new StatusCodeResult(403);
        }

        [HttpPost("v2/account/password/verify")]
        public IActionResult VerifyPasswordV2([FromForm] string userPassword)
        {
            if (!string.IsNullOrWhiteSpace(userPassword) && userPassword == "admin")
            {
                return NoContent();
            }

            return new StatusCodeResult(403);
        }
    }
}
