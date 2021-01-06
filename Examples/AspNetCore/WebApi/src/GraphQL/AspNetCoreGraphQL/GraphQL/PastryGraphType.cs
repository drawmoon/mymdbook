using AspNetCoreGraphQL.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGraphQL.GraphQL
{
    public class PastryGraphType : InterfaceGraphType<Pastry>
    {
        public PastryGraphType()
        {
            Name = nameof(Pastry);

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
