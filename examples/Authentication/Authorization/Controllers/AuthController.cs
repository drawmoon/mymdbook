using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Authorization.Controllers.V1
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="userPassword">用户密码</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckPassword([FromForm, BindRequired] string userPassword)
        {
            if (!string.IsNullOrWhiteSpace(userPassword) && userPassword == "admin")
                return Ok();
            return Forbid();
        }
    }
}