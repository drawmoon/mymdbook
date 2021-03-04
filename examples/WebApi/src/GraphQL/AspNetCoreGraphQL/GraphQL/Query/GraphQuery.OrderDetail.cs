using AspNetCoreGraphQL.GraphQL;
using AspNetCoreGraphQL.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL.Query
{
    public partial class GraphQuery
    {
        private void InitOrderDetailQuery()
        {
            Field<OrderGraphType>(nameof(OrderDetail), "根据Id获取订单明细",
                new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = nameof(OrderDetail.Id), Description = "订单明细的Id" }
                },
                context =>
                {
                    throw new System.NotImplementedException();
                });
        }
    }
}
