using GraphQLDemo.GraphQL.Core;
using GraphQLDemo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GraphQLDemo
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
            // 添加 in-memory 数据库
            var databaseName = Guid.NewGuid().ToString();
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<AppDbContext>((sp, options) => options.UseInMemoryDatabase(databaseName).UseInternalServiceProvider(sp));

            // 添加 GraphQL
            services.AddGraphQLService();

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

            // 启用 GraphQL
            app.UseGraphQLService();

            // 初始化示例数据
            using (var scope = app.ApplicationServices.CreateScope())
            {
                DataGenerator.InitSampleData(scope.ServiceProvider);
            }
        }
    }
}
