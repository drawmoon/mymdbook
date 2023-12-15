using GraphQL.Types;
using HttpApi.Entities;

namespace HttpApi.Types
{
    public class ChocolateGraphType : AbstractPastryGraphType<Chocolate>
    {
        public ChocolateGraphType()
        {
            Name = nameof(Chocolate);
            Description = "";

            Field<NonNullGraphType<IdGraphType>>(nameof(Chocolate.Id));

            Field<NonNullGraphType<StringGraphType>>(nameof(Chocolate.Name));
        }
    }
}
