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
        private void InitOrderDetailQuery()
        {
            FieldAsync<ListGraphType<OrderDetailGraphType>>("OrderDetails", "获取 OrderDetails", resolve: async context =>
            {
                var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
                return await dbContext.OrderDetails.ToListAsync();
            });

            FieldAsync<OrderGraphType>(nameof(OrderDetail), "根据 Id 获取订单明细",
                new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = nameof(OrderDetail.Id), Description = "订单明细的 Id" }
                },
                async context =>
                {
                    var id = context.GetArgument<int>(nameof(OrderDetail.Id));

                    var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
                    return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                });
        }
    }
}
