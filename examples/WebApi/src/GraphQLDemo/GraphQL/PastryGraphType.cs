using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class PastryGraphType : InterfaceGraphType<Pastry>
    {
        public PastryGraphType()
        {
            Name = nameof(Pastry);
            Description = "";

            Field<StringGraphType>(nameof(Pastry.Tag));

            AddPossibleType(new DonutGraphType());
        }
    }

    public abstract class AbstractPastryGraphType<T> : ObjectGraphType<T> where T : Pastry
    {
        public AbstractPastryGraphType()
        {
            Field<StringGraphType>(nameof(Pastry.Tag));

            Interface<PastryGraphType>();
        }
    }
}
