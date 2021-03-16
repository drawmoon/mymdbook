using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreJsonPatch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreJsonPatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPatch]
        public IActionResult Patch([FromBody] JsonPatchDocument<Order> orderPatch)
        {
            var order = new Order
            {
                Name = $"Or_{Guid.NewGuid().ToString()}",
                OrderDetails = new[]
                {
                    new OrderDetail
                    {
                        Name = "珍珠奶茶A"
                    }
                }
            };

            orderPatch.ApplyTo(order);

            return Ok(order);
        }
    }
}
