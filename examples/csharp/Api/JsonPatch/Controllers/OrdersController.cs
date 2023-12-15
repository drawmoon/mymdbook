using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using JsonPatch.Models;

namespace JsonPatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPatch]
        public IActionResult Patch([FromBody] JsonPatchDocument<Order> orderPatch)
        {
            // Sample data
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
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
