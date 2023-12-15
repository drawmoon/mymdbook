using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiDocument
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            // 定义文档
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Doc",
                Version = "v1"
            });
            
            // 描述身份验证信息
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
            });
            
            // 加载注解
            var files = new DirectoryInfo(AppContext.BaseDirectory).EnumerateFiles()
                .Where(f => f.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase));
            foreach (var fileInfo in files)
            {
                options.IncludeXmlComments(fileInfo.FullName);
            }
        }
    }
}