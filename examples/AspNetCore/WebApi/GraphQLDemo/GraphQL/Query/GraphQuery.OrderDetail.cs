using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLDemo.GraphQL.Query
{
    public partial class GraphQuery
    {
        private void InitOrderDetailQuery()
        {
            FieldAsync<ListGraphType<OrderDetailGraphType>>("OrderDetails", "获取OrderDetails", resolve: async context =>
            {
                using var dbContext = context.RequestServices.GetService<AppDbContext>();
                return await dbContext.OrderDetails.ToListAsync();
            });

            FieldAsync<OrderGraphType>(nameof(OrderDetail), "根据Id获取订单明细",
                new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = nameof(OrderDetail.Id), Description = "订单明细的Id" }
                },
                async context =>
                {
                    var id = context.GetArgument<int>(nameof(OrderDetail.Id));
                    using var dbContext = context.RequestServices.GetService<AppDbContext>();
                    return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
                });
        }
    }
}
