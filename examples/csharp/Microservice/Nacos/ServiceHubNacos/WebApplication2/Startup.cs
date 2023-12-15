using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NacosClientExtensions;
using NacosExtensions;
using WebApplication2.Services.Interfaces;

namespace WebApplication2
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
            // 添加Nacos服务
            services.AddNacosAspNetCore(Configuration);

            // 基于Nacos服务的HttpClient
            services.AddNacosTypedClient<IValuesApi>((options, sp) =>
            {
                options.ServiceName = "App1";
                options.FormatOptions.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 获取本机IP
            app.TryAddServerFeatures(Configuration);
            
            // 使用Nacos服务
            //app.UseNacosAspNetCore();

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
        }
    }
}
