using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreApiVersion
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
            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                // 启用在 Response Headers 中返回 "api-supported-versions" 和 "api-deprecated-versions"
                options.ReportApiVersions = true;

                // 表示不指定版本号时采用默认版本；如果为 false 必须指定API的版本号，例如在 QueryString 中添加 "?api-version=1.0"
                // options.AssumeDefaultVersionWhenUnspecified = true;
            });

            // 添加版本化的 API 资源管理器与 IApiVersionDescriptionProvider 服务，用于支持根据版本号暴露 Swagger 文档
            services.AddVersionedApiExplorer();

            // Swagger 文档配置
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                // 添加 xml 注释文档
                var files = new DirectoryInfo(AppContext.BaseDirectory).EnumerateFiles().Where(f => f.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase));
                foreach (var fileInfo in files)
                {
                    options.IncludeXmlComments(fileInfo.FullName);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "api-docs";
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                }
            });
        }
    }
}
