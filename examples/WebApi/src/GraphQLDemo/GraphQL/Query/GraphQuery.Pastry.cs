using GraphQL.Types;

namespace GraphQLDemo.GraphQL.Query
{
    public partial class GraphQuery
    {
        private void InitPastryQuery()
        {
            Field<ListGraphType<PastryGraphType>>("Pastrys", "获取Pastry",
                resolve: context =>
                {
                    throw new System.NotImplementedException();
                });
        }
    }
}
