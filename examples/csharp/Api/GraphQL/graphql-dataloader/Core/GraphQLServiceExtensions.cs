using GraphQL;
using GraphQL.DataLoader;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using HttpApi.Queries;
using HttpApi.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HttpApi.Core
{
    public static class GraphQLServiceExtensions
    {
        public static IServiceCollection AddGraphQLService(this IServiceCollection services)
        {
            // 添加 GraphType
            services.AddSingleton<OrderGraphType>();
            services.AddSingleton<OrderDetailGraphType>();

            services.AddSingleton<GraphQuery>();

            services.AddSingleton<ISchema, GraphQLSchema>();

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

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
