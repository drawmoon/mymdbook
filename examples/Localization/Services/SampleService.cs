using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationSample.Services
{
    public class SampleService : ISampleService
    {
        private readonly IStringLocalizer<SampleService> _localizer;

        public SampleService(IStringLocalizer<SampleService> localizer)
        {
            _localizer = localizer;
        }

        public string Get()
        {
            return _localizer["welcome {0}", "LiNin"];
        }
    }
}
