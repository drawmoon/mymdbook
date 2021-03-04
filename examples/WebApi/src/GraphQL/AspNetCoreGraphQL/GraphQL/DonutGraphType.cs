using AspNetCoreGraphQL.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL
{
    public class DonutGraphType : AbstractPastryGraphType<Donut>
    {
        public DonutGraphType()
        {
            Name = nameof(Donut);

            Field<NonNullGraphType<IntGraphType>>(nameof(Donut.Id));

            Field<NonNullGraphType<StringGraphType>>(nameof(Donut.Name));
        }
    }
}
