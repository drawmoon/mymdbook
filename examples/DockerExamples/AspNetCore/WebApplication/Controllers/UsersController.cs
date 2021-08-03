using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Entities;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public List<User> Get([FromServices] UserDbContext dbContext)
        {
            return dbContext.Users.ToList();
        }
    }
}