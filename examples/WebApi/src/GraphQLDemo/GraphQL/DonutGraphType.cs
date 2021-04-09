using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class DonutGraphType : AbstractPastryGraphType<Donut>
    {
        public DonutGraphType()
        {
            Name = nameof(Donut);
            Description = "";

            Field<NonNullGraphType<IntGraphType>>(nameof(Donut.Id));

            Field<NonNullGraphType<StringGraphType>>(nameof(Donut.Name));
        }
    }
}
