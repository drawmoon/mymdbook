using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL.Query
{
    public partial class GraphQuery : ObjectGraphType<object>
    {
        public GraphQuery()
        {
            Name = "Query";

            InitOrderQuery();
            InitOrderDetailQuery();
            InitPastryQuery();
        }
    }
}
