using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLDemo.GraphQL.Query
{
    public partial class GraphQuery
    {
        private void InitOrderQuery()
        {
            FieldAsync<ListGraphType<OrderGraphType>>("Orders", "获取Orders", resolve: async context =>
            {
                using var dbContext = context.RequestServices.GetService<AppDbContext>();
                return await dbContext.Orders.ToListAsync();
            });

            FieldAsync<OrderGraphType>(nameof(Order), "根据Id获取订单",
                new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = nameof(Order.Id), Description = "订单的Id" }
                },
                async context =>
                {
                    var id = context.GetArgument<int>(nameof(Order.Id));
                    using var dbContext = context.RequestServices.GetService<AppDbContext>();
                    return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                });
        }
    }
}
