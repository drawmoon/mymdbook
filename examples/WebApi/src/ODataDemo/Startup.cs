using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ODataDemo.Models;
using System;

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

                // 在 OData 6.0.0 及以上的版本中默认无法使用这些功能，需要在此处指定启用
                endpoints.Select().Expand().Filter().Count().OrderBy();
                // 配置 OData 的路由前缀，用 http://*:5000/odata/[controller] 访问 OData 控制器。
                endpoints.MapODataRoute("odata", "odata", AppEdmModel.GetModel());
            });

            // 初始化示例数据
            using (var scpoe = app.ApplicationServices.CreateScope())
            {
                DataGenerator.InitSampleData(scpoe.ServiceProvider);
            }
        }
    }
}
