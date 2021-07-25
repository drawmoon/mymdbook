# AspNetCore 入门

- [多个项目的全局设置](#多个项目的全局设置)
- [启用严格的编译检查](#启用严格的编译检查)
- [Swagger](#swagger)
- [中间件管道，Map 与 MapWhen](#中间件管道，map-与-mapwhen)
- [配置 Controller 允许接收空字符串](#配置-controller-允许接收空字符串)
- [配置 Controller 将空 Body 视为有效输入](#配置-controller-将空-body-视为有效输入)
- [Required 与 BindRequired 混用问题](#required-与-bindrequired-混用问题)
- [配置 Kestrel 监听的端口](#配置-kestrel-监听的端口)
- [发布部署到 Linux](#发布部署到-linux)
- [对象与流之间的序列化与反序列化](#对象与流之间的序列化与反序列化)
- [判断两个集合的元素是否相等](#判断两个集合的元素是否相等)
- [Switch 条件表达式](#switch-条件表达式)
- [HttpClient 基础用法](#httpclient-基础用法)
- [xUnit 基础用法](#xunit-基础用法)
- [内部成员对指定程序集可见](#内部成员对指定程序集可见)

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

## Required 与 BindRequired 混用问题

```csharp
public class RequiredBindingMetadataProvider : IBindingMetadataProvider
{
    public void CreateBindingMetadata(BindingMetadataProviderContext context)
    {
        if (context.PropertyAttributes?.OfType<RequiredAttribute>().Any() ?? false)
        {
            context.BindingMetadata.IsBindingRequired = true;
        }
    }
}
```

配置 `Startup`：

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
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

## 发布部署到 Linux

```bash
dotnet publish Project.sln -c Release -r linux-x64 -o published
```

在 Linux 下执行项目的可执行文件时，提示 `Couldn't find a valid ICU package installed on the system.` 报错的解决方法：

```bash
Process terminated. Couldn't find a valid ICU package installed on the system. Set the configuration flag System.Globalization.Invariant to true if you want to run with no globalization support.
   at System.Environment.FailFast(System.String)
   at System.Globalization.GlobalizationMode.GetGlobalizationInvariantMode()
   at System.Globalization.GlobalizationMode..cctor()
   at System.Globalization.CultureData.CreateCultureWithInvariantData()
   at System.Globalization.CultureData.get_Invariant()
   at System.Globalization.CultureInfo..cctor()
   at System.StringComparer..cctor()
   at System.StringComparer.get_OrdinalIgnoreCase()
   at Microsoft.Extensions.Configuration.ConfigurationProvider..ctor()
   at Microsoft.Extensions.Configuration.EnvironmentVariables.EnvironmentVariablesConfigurationSource.Build(Microsoft.Extensions.Configuration.IConfigurationBuilder)
   at Microsoft.Extensions.Configuration.ConfigurationBuilder.Build()
   at Microsoft.AspNetCore.Hosting.GenericWebHostBuilder..ctor(Microsoft.Extensions.Hosting.IHostBuilder)
   at Microsoft.Extensions.Hosting.GenericHostWebHostBuilderExtensions.ConfigureWebHost(Microsoft.Extensions.Hosting.IHostBuilder, System.Action`1<Microsoft.AspNetCore.Hosting.IWebHostBuilder>)
   at Microsoft.Extensions.Hosting.GenericHostBuilderExtensions.ConfigureWebHostDefaults(Microsoft.Extensions.Hosting.IHostBuilder, System.Action`1<Microsoft.AspNetCore.Hosting.IWebHostBuilder>)
   at WebApplication1.Program.CreateHostBuilder(System.String[])
   at WebApplication1.Program.Main(System.String[])
Aborted
```

修改 `Project.runtimeconfig.json` 文件，添加 `"System.Globalization.Invariant": true` 到文件中

```json
{
  "runtimeOptions": {
    "configProperties": {
      "System.Globalization.Invariant": true
    }
  }
}
```

## 对象与流之间的序列化与反序列化

```csharp
using System.Runtime.Serialization.Json;

public async ValueTask<Stream> Object2Stream(object value) {
    await using(MemoryStream memoryStream = new());

    DataContractJsonSerializer serializer = new(value.GetType());
    serializer.WriteObject(memoryStream, value);
    serializer.Position = 0;
    await serializer.FlushAsync();
}

public T Stream2Object<T>(Stream stream) {
    DataContractJsonSerializer serializer = new(tyoeof(T));
    var result = (TResult)serializer.ReadObject(stream);
}
```

## 判断两个集合的元素是否相等

```csharp
List<string> foo = new(){ "A", "B", "C" };
List<string> bar = new(){ "A", "B", "C" };
```

```csharp
if (foo.All(bar.Contains))
{
    return true;
}
```

## Switch 条件表达式

```csharp
var s = "abc";

var c = s switch
{
    var s1 when s1.StartsWith("a") => "a",
    var s2 when s2.Contains("b") => "b",
    var s3 when s3.EndsWith("c") => "c"
};

Console.WriteLine(c);
```

## HttpClient 基础用法

### 设置代理

```csharp
WebProxy proxy = new("127.0.0.1:8899", false)
{
    UseDefaultCredentials = false
};

HttpClient client = new(new HttpClientHandler{ Proxy = proxy }, false)
{
    BaseAddress = new Uri("http://127.0.0.1:8080")
};
```

## xUnit 基础用法

### 测试两个集合的元素是否相等

```csharp
List<string> foo = new(){ "A", "B", "C" };
List<string> bar = new(){ "A", "B", "C" };
```

测试是否全部包含

```csharp
Assert.All(foo, p => Assert.Contains(p, bar));
```

测试是否全部不包含

```csharp
Assert.All(foo, p => Assert.DoseNotContains(p, bar));
```

### 测试异常情况

```csharp
var exception = await Assert.ThrowsAsync<AppException>(async () =>
{
    await tableFieldService.Update(filed);
});

Assert.Equal("Error Message", exception.Message);
Assert.Equal(500, exception.ErrorCode);
```

## 内部成员对指定程序集可见

在单元测试的场景中，难免会碰到需要调用  `internal` 成员的情况，即可通过 `InternalsVisibleTo` 指定程序集，使该程序集可调用当前项目的 `internal` 成员。

新建项目 `FileStorage`，新建 `FSObject` 类，声明内部方法 `SetName`。

```csharp
namespace FileStorage
{
    public class FSObject
    {
        public string Name { get; internal set; }

        internal void SetName(string name)
        {
            Name = name;
        }
    }
}
```

在 `FileStorage` 项目下新建 `AssemblyInfo.cs` 文件，并指定可见的程序集 `FileStorage.Tests`。

```csharp
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FileStorage.Tests")]
```

新建单元测试项目 `FileStorage.Tests`，新建 `FSObjectTests` 类

```csharp
using FileStorage;
using Xunit;

namespace FileStorage.Tests
{
    public class FSObjectTests
    {
        [Fact]
        public void TestSetName()
        {
            FSObject obj = new();

            obj.SetName("Monica");

            Assert.Equal("Monica", obj.Name);
        }
    }
}
```
