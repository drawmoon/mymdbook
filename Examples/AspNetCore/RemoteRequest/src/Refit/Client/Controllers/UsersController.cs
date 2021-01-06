using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApi _usersApi;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5000");
            _usersApi = RestService.For<IUsersApi>(client);
        }

        [HttpGet]
        public async Task<ActionResult<string[]>> GetAll()
        {
            return await _usersApi.GetAllUser();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get([FromRoute] int id)
        {
            return await _usersApi.GetUser(id);
        }
    }
}
