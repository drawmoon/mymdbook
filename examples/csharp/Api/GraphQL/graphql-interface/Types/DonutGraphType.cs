using GraphQL.Types;
using HttpApi.Entities;

namespace HttpApi.Types
{
    public class DonutGraphType : AbstractPastryGraphType<Donut>
    {
        public DonutGraphType()
        {
            Name = nameof(Donut);
            Description = "";

            Field<NonNullGraphType<IdGraphType>>(nameof(Donut.Id));

            Field<NonNullGraphType<StringGraphType>>(nameof(Donut.Name));
        }
    }
}
