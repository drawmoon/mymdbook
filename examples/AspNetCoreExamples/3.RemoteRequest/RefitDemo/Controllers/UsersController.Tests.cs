using Microsoft.AspNetCore.Mvc;

namespace RefitDemo.Controllers
{
    [Route("api/1/users")]
    [ApiController]
    public class UsersControllerTests : ControllerBase
    {
        [HttpGet]
        public ActionResult<string[]> GetAll()
        {
            return new[] { "Randolph", "Jesse" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get([FromRoute] int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            return "Dexter";
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] string json)
        {
            return json;
        }

        [HttpPut]
        public ActionResult<string> Put([FromBody] string json)
        {
            return json;
        }

        [HttpDelete("{id}")]
        public ActionResult<int> Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            return 1;
        }
    }
}