using AuthorizationFilter.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationFilter.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class ValuesController : ControllerBase
    {
        [PasswordAuthorization("PasswordV1")] // 使用 V1 身份验证
        [HttpGet("v1/values")]
        public ActionResult<string[]> Get()
        {
            return new[] { "value1", "value2" };
        }

        [PasswordAuthorization("PasswordV2")] // 使用 V2 身份验证
        [HttpGet("v2/values")]
        public ActionResult<string[]> GetV2()
        {
            return new[] { "value1", "value2" };
        }
    }
}