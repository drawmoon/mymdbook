# ASP.NET Core Notes

- [启用可为空的类型检查](#启用可为空的类型检查)
- [将警告视为错误](#将警告视为错误)
- [多个项目的全局设置](#多个项目的全局设置)
- [中央包版本控制](#中央包版本控制)
- [判断两个集合的元素是否相等](#判断两个集合的元素是否相等)
- [字典的命名约定](#字典的命名约定)
- [将对象序列化为字节数组，与反序列化为对象](#将对象序列化为字节数组与反序列化为对象)
- [用正则表达式进行字符串替换](#用正则表达式进行字符串替换)
- [设置 HttpClient 代理](#设置-httpclient-代理)
- [中间件管道，Map 与 MapWhen](#中间件管道map-与-mapwhen)
- [Required 与 BindRequired 混用问题](#required-与-bindrequired-混用问题)
- [配置 Controller 允许接收空字符串](#配置-controller-允许接收空字符串)
- [配置 Controller 将空 Body 视为有效输入](#配置-controller-将空-body-视为有效输入)
- [Switch 使用条件表达式](#switch-使用条件表达式)

## 启用可为空的类型检查

设置所有参考类型都不可为`null`，如果需要某个参考类型可以为`null`，必须显式声明为可空类型，否则会编译警告。

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <Nullable>enable</Nullable>
    </PropertyGroup>
</Project>
```

## 将警告视为错误

在项目编译中，将所有警告消息报告为错误。比如，编译器提示某个方法可以标记为`static`时，编译项目则会报错，并且编译失败。

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    </PropertyGroup>
</Project>
```

## 多个项目的全局设置

在项目目录下创建`Directory.Build.props`文件，该文件中的配置作用解决方案中的所有项目

```xml
<Project>
    <PropertyGroup>
        <!-- 设置目标框架 -->
        <TargetFramework>net5.0</TargetFramework>
        <!-- 设置语言版本 -->
        <LangVersion>latest</LangVersion>
        <!-- 可为空的值类型 -->
        <Nullable>enable</Nullable>
        <!-- 将警告视为错误 -->
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    </PropertyGroup>
</Project>
```

## 中央包版本控制

统一的依赖包版本管理有利于项目的维护升级，在`csproj`或`Directory.Build.props`中开启此项功能

```xml
<Project>
    <ItemGroup>
        <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    </ItemGroup>
</Project>
```

在项目目录下创建`Directory.Packages.props`文件

```xml
<Project>
    <ItemGroup>
        <PackageVersion Include="Newtonsoft.Json" Version="12.0.3" />
    </ItemGroup>
</Project>
```

在`csproj`中引用依赖包

```xml
<ItemGroup>
    <PackageReference Include="Newtonsoft.Json" />
</ItemGroup>
```

## 判断两个集合的元素是否相等

```csharp
List<string> foo = new(){ "A", "B", "C" };
List<string> bar = new(){ "A", "B", "C" };

if (foo.All(bar.Contains))
{
    return true;
}
```

## 字典的命名约定

```csharp
Dictionary<string, List<string>> provincesByCountry = new();
```

## 将对象序列化为字节数组，与反序列化为对象

```csharp
// 将对象序列化为字节数组
using(MemoryStream memoryStream = new())
{
    DataContractSerializer serializer = new(tyoeof(T));

    serializer.WriteObject(memoryStream, value);

    var bytes = memoryStream.GetBuffer();
}

// 将字节数组反序列化为对象
using(MemoryStream memoryStream = new(bytes))
{
    DataContractSerializer serializer = new(tyoeof(T));

    var result = (TResult)serializer.ReadObject(memoryStream);
}
```

## 用正则表达式进行字符串替换

```csharp
var input = "COUNT({123}.{1712})";

Regex regex = new(@"({(?<table>[1-9]*[1-9][0-9]*)}\s*\.\s*)?{(?<tableField>[1-9]*[1-9][0-9]*)}", RegexOptions.Compiled);

var str = regex.Replace(input, match => $"{{{match.Groups["tableField"].Value}}}");

Console.WriteLine(str); // 输出结果为 COUNT({1712})
```

## 设置 HttpClient 代理

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

// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
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

// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    service.AddControllers(options =>
    {
        options.ModelMetadataDetailsProviders.Add(new AllowEmptyDisplayMetadataProvider());
    });
}
```

## 配置 Controller 将空 Body 视为有效输入

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.AllowEmptyInputInBodyModelBinding = true;
    });
}
```

## Switch 使用条件表达式

在 C# 8.0 及以后的版本中语法

```c#
var s = "abc";

var c = s switch
{
    var s1 when s1.StartsWith("a") => "a",
    var s2 when s2.Contains("b") => "b",
    var s3 when s3.EndsWith("c") => "c"
};

Console.WriteLine(c);
```

在 C# 7.0 及以后的版本中语法

```c#
var s = "abc";
var c = "";

switch (s)
{
    case var s1 when s1.StartsWith("a"):
        c = "a";
        break;
    case var s2 when s2.Contains("b"):
        c = "b";
        break;
    case var s3 when s3.EndsWith("c"):
        c = "c";
        break;
}

Console.WriteLine(c);
```
