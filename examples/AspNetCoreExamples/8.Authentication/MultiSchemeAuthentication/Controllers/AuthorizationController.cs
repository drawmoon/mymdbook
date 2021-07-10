using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultiSchemeAuthentication.Controllers
{
    [Route("auth")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class AuthorizationController : ControllerBase
    {
        [HttpPost("v1/password")]
        public IActionResult CheckPassword([FromForm] string userPassword)
        {
            if (!string.IsNullOrWhiteSpace(userPassword) && userPassword == "admin")
                return NoContent();
            return new StatusCodeResult(403);
        }

        [HttpPost("v2/password")]
        public IActionResult CheckPasswordV2([FromForm] string userPassword)
        {
            if (!string.IsNullOrWhiteSpace(userPassword) && userPassword == "system")
                return NoContent();
            return new StatusCodeResult(403);
        }
    }
}