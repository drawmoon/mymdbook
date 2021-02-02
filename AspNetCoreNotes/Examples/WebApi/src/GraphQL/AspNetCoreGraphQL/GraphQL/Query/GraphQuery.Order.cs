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
        private void InitOrderQuery()
        {
            Field<OrderGraphType>(nameof(Order), "根据Id获取订单",
                new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>>{ Name = nameof(Order.Id), Description = "订单的Id" }
                },
                context =>
                {
                    throw new System.NotImplementedException();
                });
        }
    }
}
