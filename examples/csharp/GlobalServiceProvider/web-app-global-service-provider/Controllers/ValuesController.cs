using GlobalServiceProvider.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalServiceProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string[]> Get()
        {
            var transientService = App.GetRequiredService<ITransientService>();
            var scopedService = App.GetRequiredService<IScopedService>();
            var singletonService = App.GetRequiredService<ISingletonService>();

            var values = App.Configuration.GetSection("Values").Get<string[]>();

            return values;
        }
    }
}