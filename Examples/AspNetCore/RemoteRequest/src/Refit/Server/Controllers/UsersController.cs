using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string[]> GetAll()
        {
            return new[] { "XiaoMing", "XiaoHong" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get([FromRoute] int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            return "XiaoMing";
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
