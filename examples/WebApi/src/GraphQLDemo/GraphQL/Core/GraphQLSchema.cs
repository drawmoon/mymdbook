using GraphQL.Types;
using GraphQL.Utilities;
using GraphQLDemo.GraphQL.Mutation;
using GraphQLDemo.GraphQL.Query;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GraphQLDemo.GraphQL.Core
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<GraphQuery>();
            Mutation = provider.GetRequiredService<GraphMutation>();

            // 使用接口类时，声明Schema有关特定类型的信息
            RegisterType(typeof(DonutGraphType));
        }
    }
}