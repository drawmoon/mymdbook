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

            // 使用接口类时，声明Schema有关特定类型的信息
            RegisterType(typeof(DonutGraphType));
        }
    }
}