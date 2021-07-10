using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ODataDemo.Models;
using System;
using Microsoft.Extensions.Options;
using OData.Swagger.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ODataDemo
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

            // 添加 OData。
            services.AddOData();
            
            // Swagger 文档配置
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            // 注册 Swagger 生成器
            services.AddSwaggerGen();
            
            // 在 ASP.NET Core 3.1 和 5.0 中使用 Swagger 配置 OData
            // https://github.com/KishorNaik/Sol_OData_Swagger_Support
            services.AddOdataSwaggerSupport();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // 启用 Swagger 中间件，用于生成 JSON 端点
            app.UseSwagger();
            // 启用 Swagger UI
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Doc");
        
                options.RoutePrefix = "api-docs";
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // 在 OData 6.0.0 及以上的版本中默认无法使用这些功能，需要在此处指定启用
                endpoints.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                // 配置 OData 的路由前缀，用 http://*:5000/api/[controller] 访问 OData 控制器。
                endpoints.MapODataRoute("api", "api", AppEdmModel.GetModel());
            });

            // 在 UseEndpoints 中配置 OData，或调用 UseOData 配置 OData
            // app.UseOData("api", "api", AppEdmModel.GetModel());

            // 初始化示例数据
            using (var scpoe = app.ApplicationServices.CreateScope())
            {
                DataGenerator.InitSampleData(scpoe.ServiceProvider);
            }
        }
    }
}
