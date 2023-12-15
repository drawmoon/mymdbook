using GraphQL.Types;
using HttpApi.Entities;
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
                var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
                return await dbContext.Orders.Where(o => o.Id == context.Source.OrderId).ToListAsync();
            });
        }
    }
}
