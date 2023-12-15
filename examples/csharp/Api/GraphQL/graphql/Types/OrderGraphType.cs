using GraphQL.Types;
using HttpApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace HttpApi.Types
{
    public class OrderGraphType : ObjectGraphType<Order>
    {
        public OrderGraphType()
        {
            Name = nameof(Order);
            Description = "Order";

            Field<NonNullGraphType<IdGraphType>>(nameof(Order.Id), "Order 的 Id");

            Field<NonNullGraphType<StringGraphType>>(nameof(Order.Name), "Order 的名称");

            FieldAsync<ListGraphType<OrderDetailGraphType>>(nameof(Order.OrderDetails), "Order 的 OrderDetail", resolve: async context =>
            {
                var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
                return await dbContext.OrderDetails.Where(o => o.OrderId == context.Source.Id).ToListAsync();
            });
        }
    }
}
