# Table of contents

- [ASP.NET Core Notes](#ASP.NET-Core-Notes)
  - [Foo，Bar，Baz 是什么意思](#Foo，Bar，Baz-是什么意思)
  - [可为空的值类型](#可为空的值类型)
  - [将警告视为错误](#将警告视为错误)
  - [判断两个集合的元素是否相等](#判断两个集合的元素是否相等)
  - [Naming convention for a C# Dictionary](#Naming-convention-for-a-C#-Dictionary)
  - [Serialize with object to MemoryStream](#Serialize-with-object-to-MemoryStream)
  - [Regex replace](#Regex-replace)
  - [设置 HTTPClient 代理](#设置-HTTPClient-代理)
  - [中间件管道，Map 与 MapWhen](#中间件管道，Map-与-MapWhen)
  - [Required 与 BindRequired 混用问题](#Required-与-BindRequired-混用问题)
  - [配置 Controller 允许接收空字符串](#配置-Controller-允许接收空字符串)
  - [配置 Controller 将空 Body 视为有效输入](#配置-Controller-将空-Body-视为有效输入)
  - [xUnit 判断两个集合的元素是否相等](#xUnit-判断两个集合的元素是否相等)

# ASP.NET Core Notes

## Foo，Bar，Baz 是什么意思

术语`foobar`，`foo`，`bar`，`baz`和`qux`经常在计算机编程、计算机相关文档中，被用作占位符的名字。当变量、函数或命令本身不太重要的时候，`foobar`，`foo`，`bar`，`baz`和`qux`就被用来充当这些实体的名字，这样做的目的仅仅是阐述一个概念，说明一个想法。

这些术语本身相对与使用的场景来说没有任何意义。

`foobar`经常被单独使用；而当需要多个实体举例的时候，`foo`，`bar`和`baz`则经常被按顺序使用。

## 可为空的值类型

> 设置所有参考类型都不可为`null`，如果需要某个参考类型可以为`null`，必须显式声明为可空类型，否则会编译警告。

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFrameworks>net5.0</TargetFrameworks>
        <!-- 启用可空性程序 -->
        <Nullable>enable</Nullable>
    </PropertyGroup>
</Project>
```

## 将警告视为错误

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFrameworks>net5.0</TargetFrameworks>
        <!-- 将警告视为错误 -->
        <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    </PropertyGroup>
</Project>
```

## 判断两个集合的元素是否相等

```csharp
List<string> foo = new{ "A", "B", "C" };
List<string> bar = new{ "A", "B", "C" };

if (foo.All(bar.Contains))
{
  return true;
}
```

## Naming convention for a C# Dictionary

```csharp
Dictionary<string, List<string>> provincesByCountry = new();
```

## Serialize with object to MemoryStream

```csharp
// Serialize
using(MemoryStream memoryStream = new())
{
    DataContractSerializer serializer = new(tyoeof(T));

    serializer.WriteObject(memoryStream, value);

    var bytes = memoryStream.GetBuffer();
}

// Deserialize
using(MemoryStream memoryStream = new(bytes))
{
    DataContractSerializer serializer = new(tyoeof(T));
    
    var result = (TResult)serializer.ReadObject(memoryStream);
}
```

## Regex replace

```csharp
Regex regex = new(@"({(?<tableId>[1-9]*[1-9][0-9]*)}\s*\.\s*)?{(?<fieldId>[1-9]*[1-9][0-9]*)}", RegexOptions.Compiled);

var str = regex.Replace("COUNT({123}.{1712})", p => $"{{{p.Groups["fieldId"].Value}}}");

Console.WriteLine(str); // COUNT({1712})
```

## 设置 HTTPClient 代理

```csharp
WebProxy proxy = new("127.0.0.1:8899", false)
{
    UseDefaultCredentials = false
};

HttpClient client = new(new{ Proxy = proxy }, false)
{
    BaseAddress = new("http://127.0.0.1:8080")
};
```

## 中间件管道，Map 与 MapWhen

### Map

用于约定来创建管道分支。`Map`基于给定请求路径的匹配项来创建请求管道分支。如果请求路径以给定路径开头，则执行分支。

### MapWhen

基于给定谓词的结果创建请求管道分支。

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.MapWhen(p => p.Request.Path.StartsWithSegments("/api/values"), innerApp =>
    {
        innerApp.Run(async context =>
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(new string[] { "value1", "value2" }));
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
```

配置`MVC`选项

```csharp
services.AddMvcCore(options =>
{
    options.ModelMetadataDetailsProviders.Add(new RequiredBindingMetadataProvider());
});
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

配置`MVC`选项

```csharp
service.AddMvcCore(options =>
{
    options.ModelMetadataDetailsProviders.Add(new AllowEmptyDisplayMetadataProvider());
});
```

## 配置 Controller 将空 Body 视为有效输入

```csharp
services.AddMvcCore(options =>
{
    options.AllowEmptyInputInBodyModelBinding = true;
});
```

## xUnit 判断两个集合的元素是否相等

```csharp
List<string> foo = new{ "A", "B" };
List<string> bar = new{ "A" };

// 是否全部包含
Assert.All(foo, p => Assert.Contains(p, bar));

// 是否全部不包含
Assert.All(foo, p => Assert.DoseNotContains(p, bar));
```
