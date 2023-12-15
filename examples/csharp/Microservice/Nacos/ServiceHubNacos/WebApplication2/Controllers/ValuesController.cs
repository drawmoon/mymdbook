using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services.Interfaces;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesApi _valuesHttpApi;

        public ValuesController(IValuesApi valuesHttpApi)
        {
            _valuesHttpApi = valuesHttpApi;
        }

        [HttpGet]
        public ActionResult<string[]> Get()
        {
            return Ok(_valuesHttpApi.Get().GetAwaiter().GetResult());
        }
    }
}
