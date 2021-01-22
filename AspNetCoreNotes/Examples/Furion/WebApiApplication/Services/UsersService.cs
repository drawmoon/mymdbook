using Furion.DynamicApiController;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApiApplication.Exceptions;

namespace WebApiApplication.Services
{
    [ApiDescriptionSettings(Version = "2.0")]
    public class UsersService : IDynamicApiController // 动态 WebApi
    {
        public List<string> GetAll() => new List<string>();

        public string Get(string id) => "abc";

        [ApiDescriptionSettings(Name = "Roles")] // 自定义接口名称
        public List<string> GetUserRoles(
            // 自定义参数位置
            [ApiSeat(ApiSeats.ControllerEnd)] string id)
        {
            // 友好的异常处理
            throw Oops.Oh(ErrorCodes.UserNotFound, id);
        }

        [ApiDescriptionSettings(Name = "Organizations")]
        public ActionResult<string> GetUserOrganizations(
            [ApiSeat(ApiSeats.ControllerEnd)] string id) => new StatusCodeResult((int) HttpStatusCode.Forbidden);

        [ApiDescriptionSettings(Name = "Claims")]
        public ActionResult<string> GetUserClaims(
            [ApiSeat(ApiSeats.ControllerEnd)] string id) => new StatusCodeResult((int) HttpStatusCode.Unauthorized);
    }
}
