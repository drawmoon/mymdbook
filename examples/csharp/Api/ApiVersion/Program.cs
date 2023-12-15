using ApiVersionify;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiVersioning(options =>
{
    // 启用在 Response Headers 中返回 "api-supported-versions" 和 "api-deprecated-versions"
    options.ReportApiVersions = true;

    // 表示不指定版本号时采用默认版本；如果为 false 必须指定 API 的版本号，例如在 QueryString 中添加 "?api-version=1.0"
    // options.AssumeDefaultVersionWhenUnspecified = true;
});

// 添加版本化的 API 资源管理器与 IApiVersionDescriptionProvider 服务，用于支持根据版本号暴露 Swagger 文档
builder.Services.AddVersionedApiExplorer();

// Swagger 相关配置
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Swagger 相关配置
using (var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

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

app.MapControllers();

app.Run();