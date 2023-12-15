using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OData
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            // 这里可以定义一个或多个文档，比如v1、v2等等
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Doc",
                Version = "v1"
            });

            // 描述 API 的身份验证
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
            });

            // 加载注释文件
            var files = new DirectoryInfo(AppContext.BaseDirectory).EnumerateFiles()
                .Where(f => f.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase));
            foreach (var fileInfo in files)
            {
                options.IncludeXmlComments(fileInfo.FullName);
            }
        }
    }
}