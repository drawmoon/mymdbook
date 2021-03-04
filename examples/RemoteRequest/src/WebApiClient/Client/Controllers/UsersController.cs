using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersWebApi _usersWebApi;

        public UsersController(IUsersWebApi usersWebApi)
        {
            _usersWebApi = usersWebApi;
        }

        [HttpGet]
        public async Task<ActionResult<string[]>> GetAll()
        {
            return await _usersWebApi.GetAllUser();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get([FromRoute] int id)
        {
            return await _usersWebApi.GetUser(id);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] string json)
        {
            return await _usersWebApi.AddUser(json);
        }

        [HttpPut]
        public async Task<ActionResult<string>> Put([FromBody] string json)
        {
            return await _usersWebApi.UpdateUser(json);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            return await _usersWebApi.DeleteUser(id);
        }
    }
}