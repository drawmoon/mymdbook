using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using GraphQLDemo.GraphQL.Mutation;
using GraphQLDemo.GraphQL.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLDemo.GraphQL.Core
{
    public static class GraphQLServiceExtensions
    {
        public static IServiceCollection AddGraphQLService(this IServiceCollection services)
        {
            // 添加 GraphQLType
            services.AddSingleton<OrderGraphType>();
            services.AddSingleton<OrderDetailGraphType>();
            services.AddSingleton<PastryGraphType>();
            services.AddSingleton<DonutGraphType>();

            services.AddSingleton<GraphQuery>();
            services.AddSingleton<GraphMutation>();

            services.AddSingleton<ISchema, GraphQLSchema>();

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            return services;
        }

        public static IApplicationBuilder UseGraphQLService(this IApplicationBuilder app)
        {
            // 启用 GraphQL 中间件用于处理 GraphQL 请求
            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            {
                Path = "/api/graphql",
                BuildUserContext = ctx => new GraphQLUserContext
                {
                    User = ctx.User
                },
                EnableMetrics = true
            });

            return app;
        }
    }
}
