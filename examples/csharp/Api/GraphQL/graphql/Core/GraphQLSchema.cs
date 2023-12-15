using GraphQL.Types;
using GraphQL.Utilities;
using HttpApi.Queries;
using HttpApi.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HttpApi.Core
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<GraphQuery>();
        }
    }
}