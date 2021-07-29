# AspNetCore 入门

- [中间件管道，Map 与 MapWhen](#中间件管道，map-与-mapwhen)
- [配置 Controller 允许接收空字符串](#配置-controller-允许接收空字符串)
- [配置 Controller 将空 Body 视为有效输入](#配置-controller-将空-body-视为有效输入)
- [配置 Kestrel 监听的端口](#配置-kestrel-监听的端口)
- [Swagger](#swagger)
- [多个项目的全局设置](#多个项目的全局设置)
- [启用严格的编译检查](#启用严格的编译检查)
- [发布部署到 Linux](#发布部署到-linux)

## 中间件管道，Map 与 MapWhen

### Map

用于约定来创建管道分支。`Map`基于给定请求路径的匹配项来创建请求管道分支。如果请求路径以给定路径开头，则执行分支。

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.Map("/swagger", appBuilder =>
    {
        appBuilder.UseSwagger();
    });
}
```

### MapWhen

基于给定谓词的结果创建请求管道分支。

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.MapWhen(httpContext => httpContext.Request.Path.StartsWithSegments("/api/values"), appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            await context.Response.WriteJsonAsync(new[]{"value1", "value2"});
        });
    });
}
```

## 配置 Controller 允许接收空字符串

```csharp
public class AllowEmptyDisplayMetadataProvider : IMetadataDetailsProvider, IDisplayMetadataProvider
{
    public void CreateDisplayMetadata(DisplayMetadataProviderContetx context)
    {
        if (context.Key.MetadataKind == ModelMetadataKind.Parameter)
        {
            context.DisplayMetadata.ConvertEmptyStringToNull = false;
        }
    }
}
```
配置 `Startup`：

```csharp
public void ConfigureServices(IServiceCollection services)
{
    service.AddControllers(options =>
    {
        options.ModelMetadataDetailsProviders.Add(new AllowEmptyDisplayMetadataProvider());
    });
}
```

## 配置 Controller 将空 Body 视为有效输入

配置 `Startup`：

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.AllowEmptyInputInBodyModelBinding = true;
    });
}
```

## 配置 Kestrel 监听的端口

修改 `appsettings.json` 配置文件

```json
{
  "Kestrel": {
    "EndPoints": {
      "Http": {
        "Url": "http://0.0.0.0:3001"
      }
    }
  }
}
```

## Swagger

> Swagger、Redoc 是基于 OpenAPI 规范的工具，例如生成 RESTful API 文档、API 调用等。

安装

```bash
dotnet add package Swashbuckle.AspNetCore --version 6.1.4
```

配置控制器分组，`GroupName`与`SwaggerDoc`中的`name`对应

```csharp
[ApiExplorerSettings(GroupName = "v1")] // 指定控制器分组名称
[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{

}
```

编辑项目文件，启用 XML 注释，并忽略 1591 警告码

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

新建`ConfigureSwaggerOptions`类，配置 Swagger 文档信息

```csharp
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
```

注册 Swagger 生成器

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Swagger 文档配置
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    // 注册 Swagger 生成器
    services.AddSwaggerGen();

    services.AddControllers();
}
```

启用 Swagger、Swagger UI

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // 启用 Swagger 中间件，用于生成 JSON 端点
    app.UseSwagger();
    // 启用 Swagger UI
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Doc");
        
        options.RoutePrefix = "api-docs";
    });

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

![Swagger 效果图](../images/swagger.png)

启用 ReDoc UI

安装

```bash
dotnet add package Swashbuckle.AspNetCore.ReDoc --version 6.1.4
```

启用 Swagger、ReDoc UI

```csharp
public class startup
{
  public void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
    // 启用 Swagger 中间件，用于生成 JSON 端点
    app.UseSwagger();
    // 启用 ReDoc UI
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

![ReDoc 效果图](../images/redoc.png)

## 多个项目的全局设置

### 全局的项目设置

在项目根目录下创建 `Directory.Build.props` 文件，该文件中的配置作用解决方案中的所有项目。

```xml
<Project>
    <PropertyGroup>
        <TargetFramework>net5.0</TargetFramework>
        <LangVersion>latest</LangVersion>
        <Nullable>enable</Nullable>
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    </PropertyGroup>
</Project>
```

### 中央包版本控制

统一的依赖包版本管理有利于项目的维护升级，在项目编辑或 `Directory.Build.props` 中开启此项功能。

```xml
<Project>
    <ItemGroup>
        <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    </ItemGroup>
</Project>
```

在项目目录下创建 `Directory.Packages.props` 文件，`PackageVersion` 指定引用的包以及版本号。

```xml
<Project>
    <ItemGroup>
        <PackageVersion Include="Newtonsoft.Json" Version="12.0.3" />
    </ItemGroup>
</Project>
```

在项目中引用依赖包使用 `PackageReference`。

```xml
<ItemGroup>
    <PackageReference Include="Newtonsoft.Json" />
</ItemGroup>
```

## 启用严格的编译检查

### 启用可为空的类型检查

设置所有类型都不可为 `null`，如果需要某个类型可以为 `null`，必须显式声明为可空类型，否则会编译警告。

在 `<PropertyGroup>` 下添加 `<Nullable>` 项目设置，`enable` 为启用，`disable` 为禁用。

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <Nullable>enable</Nullable>
    </PropertyGroup>
</Project>
```

启用可为空的类型检查后，所有的可空类型的声明必须添加 `?`:

```csharp
string notNull = "Hello";
string? nullable = null;
```

### 将警告视为错误

在项目编译中，将所有警告消息报告为错误。

在 `<PropertyGroup>` 下添加 `<TreatWarningsAsErrors>` 项目设置，`true` 为启用，`false` 为禁用。

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    </PropertyGroup>
</Project>
```

## 发布部署到 Linux

```bash
dotnet publish MyProject.sln -c Release -r linux-x64 -o published
```

### 通过 dotnet 指令启动程序

```bash
dotnet MyProject.dll
```

### 通过可执行文件启动程序

```bash
chmod +x MyProject && MyProject
```

遇到了 `Couldn't find a valid ICU package installed on the system.` 报错？

修改 `Project.runtimeconfig.json` 文件，添加 `"System.Globalization.Invariant": true` 到文件中：

```json
{
  "runtimeOptions": {
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
```
