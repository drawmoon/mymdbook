using GraphQL.Types;

namespace GraphQLDemo.GraphQL.Query
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
