using GraphQL.Types;
using GraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace GraphQLDemo.GraphQL
{
    public class OrderGraphType : ObjectGraphType<Order>
    {
        public OrderGraphType()
        {
            Name = nameof(Order);
            Description = "Order";

            Field<NonNullGraphType<IdGraphType>>(nameof(Order.Id), "Order的Id");

            Field<NonNullGraphType<StringGraphType>>(nameof(Order.Name), "Order的名称");

            FieldAsync<ListGraphType<OrderDetailGraphType>>(nameof(Order.OrderDetails), "Order的OrderDetail", resolve: async context =>
            {
                using var dbContext = context.RequestServices.GetService<AppDbContext>();
                return await dbContext.OrderDetails.Where(o => o.OrderId == context.Source.Id).ToListAsync();
            });
        }
    }
}
