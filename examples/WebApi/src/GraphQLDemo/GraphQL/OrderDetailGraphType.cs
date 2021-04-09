using GraphQL.Types;
using GraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace GraphQLDemo.GraphQL
{
    public class OrderDetailGraphType : ObjectGraphType<OrderDetail>
    {
        public OrderDetailGraphType()
        {
            Name = nameof(OrderDetail);
            Description = "OrderDetail";

            Field<NonNullGraphType<IdGraphType>>(nameof(OrderDetail.Id), "OrderDetail的Id");

            Field<NonNullGraphType<StringGraphType>>(nameof(OrderDetail.Name), "OrderDetail的名称");

            FieldAsync<OrderGraphType>(nameof(OrderDetail.Order), "OrderDetail所属的Order", resolve: async context =>
            {
                using var dbContext = context.RequestServices.GetService<AppDbContext>();
                return await dbContext.Orders.Where(o => o.Id == context.Source.OrderId).ToListAsync();
            });
        }
    }
}
