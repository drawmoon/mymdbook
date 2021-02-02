using System;
using AspNetCoreGraphQL.GraphQL.Mutation;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using AspNetCoreGraphQL.GraphQL.Query;

namespace AspNetCoreGraphQL.GraphQL.Core
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<GraphQuery>();
            Mutation = provider.GetRequiredService<GraphQLMutation>();

            // 使用接口类时，声明Schema有关特定类型的信息
            RegisterType<DonutGraphType>();
        }
    }
}