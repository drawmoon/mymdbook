using GraphQL.Types;

namespace HttpApi.Queries
{
    public partial class GraphQuery : ObjectGraphType<object>
    {
        public GraphQuery()
        {
            Name = "Query";

            InitPastryQuery();
        }
    }
}
