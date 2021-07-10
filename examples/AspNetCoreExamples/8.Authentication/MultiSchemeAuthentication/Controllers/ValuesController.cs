using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiSchemeAuthentication.Authentication;

namespace MultiSchemeAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class ValuesController : ControllerBase
    {
        [PasswordAuthorization("PasswordAuthenticationV1")] // 使用 V1 身份验证
        [HttpGet("v1")]
        public ActionResult<string[]> Get()
        {
            return new[] { "value1", "value2" };
        }
        
        [PasswordAuthorization("PasswordAuthenticationV2")] // 使用 V2 身份验证
        [HttpGet("v2")]
        public ActionResult<string[]> GetV2()
        {
            return new[] { "value1", "value2" };
        }
    }
}