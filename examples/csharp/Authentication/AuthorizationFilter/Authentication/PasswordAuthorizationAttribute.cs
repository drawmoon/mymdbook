using Microsoft.AspNetCore.Mvc;

namespace AuthorizationFilter.Authentication
{
    /// <summary>
    /// 控制器、方法层的密码验证处理程序
    /// </summary>
    public class PasswordAuthorizationAttribute : TypeFilterAttribute
    {
        public PasswordAuthorizationAttribute(string scheme) : base(typeof(PasswordAuthorizationFilter))
        {
            Arguments = new object[] { scheme };
        }
    }
}