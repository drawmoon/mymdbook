using GraphQL.Types;

namespace GraphQLDemo.GraphQL.Mutation
{
    public partial class GraphMutation : ObjectGraphType<object>
    {
        public GraphMutation()
        {
            InitOrder();
        }
    }
}
