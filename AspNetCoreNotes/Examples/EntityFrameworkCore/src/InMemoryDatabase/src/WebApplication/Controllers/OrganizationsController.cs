using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationSample.Models;

namespace WebApplicationSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Organization>>> GetAll([FromServices] AppDbContext context)
        {
            return await context.Organizations.ToListAsync();
        }
    }
}
