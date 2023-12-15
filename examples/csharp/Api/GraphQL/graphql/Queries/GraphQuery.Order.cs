using GraphQL;
using GraphQL.Types;
using HttpApi.Entities;
using HttpApi.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HttpApi.Queries
{
    public partial class GraphQuery
    {
        private void InitOrderQuery()
        {
            FieldAsync<ListGraphType<OrderGraphType>>("Orders", "获取 Orders", resolve: async context =>
            {
                var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
                return await dbContext.Orders.ToListAsync();
            });

            FieldAsync<OrderGraphType>(nameof(Order), "根据 Id 获取订单",
                new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = nameof(Order.Id), Description = "订单的 Id" }
                },
                async context =>
                {
                    var id = context.GetArgument<int>(nameof(Order.Id));

                    var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
                    return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                });
        }
    }
}
