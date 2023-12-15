using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using HttpApi.Entities;
using HttpApi.Services;
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

                    var accessor = context.RequestServices.GetRequiredService<IDataLoaderContextAccessor>();
                    var orderService = context.RequestServices.GetRequiredService<IOrderService>();

                    var loader = accessor.Context.GetOrAddLoader($"{nameof(IOrderService.Get)}/{id}", () => orderService.Get(id));
                    return await loader.LoadAsync().GetResultAsync();
                });
        }
    }
}
