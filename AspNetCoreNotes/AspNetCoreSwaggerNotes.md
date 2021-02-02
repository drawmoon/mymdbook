# Table of contents

- [ASP.NET Core Swagger Notes](#ASP.NET-Core-Swagger-Notes)
  - [介绍](#介绍)
  - [添加并配置 Swagger](#添加并配置-Swagger)
  - [启用 Swagger-UI](#启用-Swagger-UI)
  - [启用 ReDoc-UI](#启用-ReDoc-UI)

# ASP.NET Core Swagger Notes

## 介绍

Swagger 是基于 OpenAPI 规范的工具，例如生成 RESTful API 文档、API 调用等。

## 添加并配置 Swagger

注册 Swagger 生成器

```csharp
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddSwaggerGen(options => {
      // 这里可以定义一个或多个文档，比如v1、v2等等
      options.SwaggerDoc("v1", new OpenApiInfo
      {
          Title = "API Doc",
          Version = "v1"
      });
    });
  }
}
```

启用 Swagger

```csharp
public class Startup
{
  public void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
    // 启用 Swagger 中间件，用于生成 JSON 端点
    app.UseSwagger();
  }
}
```

配置 Controller

```csharp
// 指定控制器分组
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{

}
```

### API 文档注释

编辑项目文件，启用`GenerateDocumentationFile`，并忽略 1591 警告码。

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

```csharp
services.AddSwaggerGen(options =>
{
  // 指定注释文件的路径
  var files = new DirectoryInfo(AppContext.BaseDirectory).EnumerateFiles()
    .Where(f => f.Extension.Equals(".xml", StringComparison.OrdinalIgnoreCase));
  foreach (var fileInfo in files)
  {
      options.IncludeXmlComments(fileInfo.FullName);
  }
});
```

### 描述 API 的身份验证

```csharp
services.AddSwaggerGen(options => 
{
  options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey,
      Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
  });
});
```

### 类型映射

```csharp
services.AddSwaggerGen(options => {
  options.MapType<ContentType>(() => new Schema
  {
      Type = "Enum",
      Enum = typeof(ContentType).GetEnumValues().Cast<ContentType>().Select(t => (object)$"{t}={(int)t}").ToList(),
      Description = "内容类型"
  });
});
```

## 启用 Swagger-UI

```csharp
public class startup
{
  public void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
    // Swagger 资源，包括 html、js、css 等
    app.UseSwaggerUI(options => {
      // 定义 Swagger 的 JSON 端点
      options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API V1");

      // API 文档的路由前缀
      options.RoutePrefix = "api-docs";
    });
  }
}
```

## 启用 ReDoc-UI

```csharp
public class startup
{
  public void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
    // ReDoc 资源，包括 html、js、css 等
    app.UseReDoc(options =>
    {
      // 定义 Swagger 的 JSON 端点
      options.SpecUrl = "/swagger/v1/swagger.json";

      // API 文档的标题
      options.DocumentTitle = "Sample API";

      // API 文档的路由前缀
      options.RoutePrefix = "api-docs";
    }
  }
}
```
