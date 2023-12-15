using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using HttpApi.Queries;
using HttpApi.Types;

namespace HttpApi.Core
{
    public static class GraphQLServiceExtensions
    {
        public static IServiceCollection AddGraphQLService(this IServiceCollection services)
        {
            // 添加 GraphType
            services.AddSingleton<PastryGraphType>();
            services.AddSingleton<DonutGraphType>();

            services.AddSingleton<GraphQuery>();

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
