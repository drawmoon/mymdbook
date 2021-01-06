using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizationSample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IStringLocalizer<SampleController> _localizer;
        private readonly ISampleService _simpleService;

        public SampleController(IStringLocalizer<SampleController> localizer, ISampleService simpleService)
        {
            _localizer = localizer;
            _simpleService = simpleService;
        }

        //[HttpGet]
        //public ActionResult<string> Get()
        //{
        //    return _localizer["welcome {0}", "LiNin"].Value;
        //}

        [HttpGet]
        public ActionResult<string> Get()
        {
            return _simpleService.Get();
        }
    }
}