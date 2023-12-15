using GraphQL.Types;
using HttpApi.Entities;

namespace HttpApi.Types
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
