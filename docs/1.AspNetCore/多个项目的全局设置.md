# 多个项目的全局设置

## 全局的项目设置

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

## 中央包版本控制

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
