using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localization.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Localization.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly IUserService _userService;

        public UsersController(IStringLocalizer<UsersController> localizer, IUserService simpleService)
        {
            _localizer = localizer;
            _userService = simpleService;
        }

        //[HttpGet]
        //public ActionResult<string> Get()
        //{
        //    return _localizer["welcome {0}", "LiNin"].Value;
        //}

        [HttpGet]
        public ActionResult<string> Get()
        {
            return _userService.Get();
        }
    }
}