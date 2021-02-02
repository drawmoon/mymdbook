using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreGraphQL.GraphQL.Core;
using AspNetCoreGraphQL.GraphQL.Mutation;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using AspNetCoreGraphQL.GraphQL.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreGraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: 报错
            services.AddSingleton<GraphQL.OrderGraphType>();
            services.AddSingleton<GraphQL.OrderDetailGraphType>();
            services.AddSingleton<GraphQL.PastryGraphType>();

            #region GraphQL

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<ISchema, GraphQLSchema>();

            services.AddSingleton<GraphQuery>();
            services.AddSingleton<GraphQLMutation>();

            #endregion

            // TODO: 修改Read方式
            services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddHttpContextAccessor();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region GraphQL

            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            {
                Path = "/api/graphql",
                BuildUserContext = ctx => new GraphQLUserContext
                {
                    User = ctx.User
                },
                EnableMetrics = true
            });

            #endregion
        }
    }
}
