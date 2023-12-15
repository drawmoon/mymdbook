using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using OData.Models;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

namespace OData.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ODataController
    {
        private readonly AppDbContext _dbContext;

        public UsersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [EnableQuery] // 启用 OData 查询功能
        public IActionResult Get()
        {
            // 返回 IQueryable 可使 OData 使用 EFCore 的功能将查询转换为 SQL 查询
            // 如果是 IEnumerable 则是在内存中执行查询
            return Ok(_dbContext.Users);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{id}/orders")]
        [EnableQuery]
        public async Task<IActionResult> GetOrders([FromRoute] int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_dbContext.Orders.Where(o => o.UserId == id));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                return BadRequest();
            }

            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}
