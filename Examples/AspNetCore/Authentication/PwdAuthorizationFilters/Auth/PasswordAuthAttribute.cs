using System;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Auth
{
    /// <summary>
    /// 控制器、方法层的密码验证处理程序
    /// </summary>
    public class PasswordAuthAttribute : TypeFilterAttribute
    {
        public PasswordAuthAttribute(string scheme) : base(typeof(PasswordAuthFilter))
        {
            Arguments = new object[] { scheme };
        }
    }
}