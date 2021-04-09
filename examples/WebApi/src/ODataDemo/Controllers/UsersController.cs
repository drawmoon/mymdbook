using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataDemo.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
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
    }
}
