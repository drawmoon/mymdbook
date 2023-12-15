using GraphQL.DataLoader;
using GraphQL.Types;
using HttpApi.Entities;
using HttpApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace HttpApi.Types
{
    public class OrderDetailGraphType : ObjectGraphType<OrderDetail>
    {
        public OrderDetailGraphType()
        {
            Name = nameof(OrderDetail);
            Description = "OrderDetail";

            Field<NonNullGraphType<IdGraphType>>(nameof(OrderDetail.Id), "OrderDetail 的 Id");

            Field<NonNullGraphType<StringGraphType>>(nameof(OrderDetail.Name), "OrderDetail 的名称");

            FieldAsync<OrderGraphType>(nameof(OrderDetail.Order), "OrderDetail 所属的 Order", resolve: async context =>
            {
                var orderId = context.Source.OrderId;

                var accessor = context.RequestServices.GetRequiredService<IDataLoaderContextAccessor>();
                var orderService = context.RequestServices.GetRequiredService<IOrderService>();

                var loader = accessor.Context.GetOrAddLoader($"{nameof(IOrderService.Get)}/{orderId}", () => orderService.Get(orderId));
                return await loader.LoadAsync().GetResultAsync();
            });
        }
    }
}
