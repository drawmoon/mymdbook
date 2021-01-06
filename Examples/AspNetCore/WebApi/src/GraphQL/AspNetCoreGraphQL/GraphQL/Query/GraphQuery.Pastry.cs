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
        private void InitPastryQuery()
        {
            Field<PastryGraphType>(nameof(Pastry), "获取Pastry",
                resolve: context =>
                {
                    throw new System.NotImplementedException();
                });
        }
    }
}
