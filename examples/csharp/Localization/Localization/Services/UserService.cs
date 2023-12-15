using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localization.Services
{
    public class UserService : IUserService
    {
        private readonly IStringLocalizer<UserService> _localizer;

        public UserService(IStringLocalizer<UserService> localizer)
        {
            _localizer = localizer;
        }

        public string Get()
        {
            return _localizer["welcome {0}", "Lively"];
        }
    }
}
