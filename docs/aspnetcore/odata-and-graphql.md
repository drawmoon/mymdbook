# OData 与 GraphQL

- [OData](#odata)
- [GraphQL](#graphql)

## OData

> OData 定义了一组构建和使用 RESTful API 的最佳实践。 OData 可帮助您在构建 RESTful API 时专注于业务逻辑，而不必担心定义请求和响应头，状态代码，HTTP 方法，URL 约定，媒体类型，有效载荷格式，查询选项等的各种方法。OData 还提供跟踪更改，定义可重用过程的功能/动作以及发送异步/批处理请求的指南。

下面将创建一个基于 OData 的`ASP.NET Core API`项目，创建 OData 模型，使用 OData 的查询选项，例如`按需查询`、`扩展查询`、`过滤查询`等。

[本文示例项目地址](https://github.com/drawmoon/mynotes/tree/master/examples/WebApi/src/ODataDemo)

### 安装 Nuget 程序包

- `Microsoft.AspNetCore.OData`
- `Microsoft.OData.ModelBuilder`
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.InMemory`

### 创建实体和数据库上下文

新建一个用户实体和订单实体

```c#
public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public string Email { get; set; }

    public bool LockoutEnabled { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}

public class OrderDetail
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; }
}
```

新建`AppDbContext.cs`文件，创建数据库上下文

```c#
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails);
    }
}
```

将数据库上下文添加到容器中

```c#
public void ConfigureServices(IServiceCollection services)
{
    // 添加 in-memory 数据库
    var databaseName = Guid.NewGuid().ToString();
    services
        .AddEntityFrameworkInMemoryDatabase()
        .AddDbContext<AppDbContext>((sp, options) => options.UseInMemoryDatabase(databaseName).UseInternalServiceProvider(sp));

    services.AddControllers();
}
```

### 创建 OData 模型

新建一个`AppEdmModel.cs`文件，将实体添加 OData 模型中。\
在`AppEdmModel`类中可以指定实体的 CRUD 操作权限，权限声明添加到 OData 模型中后，授权中间件将会读取这些权限声明，处理不同请求所需要的权限范围。

```c#
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace ODataDemo.Models
{
    public static class AppEdmModel
    {
        public static IEdmModel GetModel()
        {
            ODataConventionModelBuilder builder = new();
            var users = builder.EntitySet<User>("Users");

            users
                .HasReadRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Read")))
                .HasReadByKeyRestrictions(r =>
                    r.HasPermissions(p => p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.ReadByKey"))));

            users
                .HasInsertRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Create")));

            users
                .HasUpdateRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Update")));

            users.HasDeleteRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Delete")));

            builder.EntitySet<Order>("Orders");

            builder.EntitySet<OrderDetail>("OrderDetails");

            return builder.GetEdmModel();
        }
    }
}
```

### 添加 OData 服务

在`Startup.cs`中配置 OData 服务，和指定 OData 的接口路由

```c#
public void ConfigureServices(IServiceCollection services)
{
    // 添加 OData。
    services.AddOData();

    services.AddControllers();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();

        // 在 OData 6.0.0 及以上的版本中默认无法使用这些功能，需要在此处指定启用
        endpoints.Select().Expand().Filter().Count().OrderBy();
        // 配置 OData 的路由前缀，用 http://*:5000/odata/[controller] 访问 OData 控制器。
        endpoints.MapODataRoute("odata", "odata", AppEdmModel.GetModel());
    });
}
```

### 创建控制器

新建一个`UsersController.cs`文件，并继承`ODataController`基类

```c#
public class UsersController : ODataController
{
    private readonly AppDbContext _dbContext;

    public UsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    [EnableQuery] // 启用 OData 查询功能
    public IActionResult Get()
    {
        // 返回 IQueryable 可使 OData 使用 EFCore 的功能将查询转换为 SQL 查询
        // 如果是 IEnumerable 则是在内存中执行查询
        return Ok(_dbContext.Users);
    }
}
```

### 按需查询 $select

OData 支持请求获取一个复杂对象中指定的属性，在接口传递`$select`参数实现。

| 描述 | 示例 |
| --- | --- |
| 获取用户信息，返回结果只包含`Id`和`Name`字段 | /odata/users?$Select=Id,Name |

### 过滤查询 $filter

OData 支持过滤请求的资源的集合，在接口传递`$filter`参数实现。OData 定义了一组逻辑运算符，结果为`true`或`false`，逻辑运算符不支持集合、实体和复杂类型的操作数。

| 运算符 | 描述 | 示例 |
| --- | --- | --- |
| eq | Equals，判断左右表达式是否相等。获取用户列表，查询`LockoutEnabled = true`的用户 | /odata/users?$Filter=LockoutEnabled eq false |
| ne | Not Equals，判断左右表达式是否不相等。获取用户列表，查询`LockoutEnabled != true`的用户 | /odata/users?$Filter=LockoutEnabled ne true |
| contains | 判断某个属性的值是否包含指定字符串。获取用户列表，查询用户名包含`xiao`的用户 | /odata/users?$Filter=contains(Name, 'xiao') |
| gt | Greater than，大于。获取用户列表，查询年龄大于 20 的用户 | /odata/users?$filter=Age gt 20 |
| ge | Greater than or equal，大于或等于。获取用户列表，查询年龄大于或等于 20 的用户 | /odata/users?$filter=Age ge 20 |
| lt | Less than，小于。获取用户列表，查询年龄小于 20 的用户 | /odata/users?$filter=Age lt 20 |
| le | Less than or equal，小于或等于。获取用户列表，查询年龄小于或等于 20 的用户 | /odata/users?$filter=Age le 20 |
| and | 左右表达式都必须满足条件。获取用户列表，用户名等于`xiaol`，并且`LockoutEnabled`等于`false` | /odata/users?$Filter=Name eq 'xiaol' and LockoutEnabled eq false |
| or | 左右表达式任意一个满足条件。获取用户列表，用户名等于`xiaol`，或者用户名等于`chonya` | /odata/users?$Filter=Name eq 'xiaol' or Name eq 'chonya' |
| not | 取反。获取用户列表，用户名不以`xiao`开头的用户列表 | /odata/users?$Filter=not startswith(Name, 'xiao') |
| in | 判断是否存在指定的集合中。获取用户列表，用户名等于`xiaol`、`chonya`的用户 | /odata/users?$Filter=Name in ('xiaol', 'chonya') |

### 扩展查询 $expand

OData 支持检索资源包含的相关资源，在接口传递`$expand`参数实现。

| 描述 | 示例 |
| --- | --- |
| 获取用户列表，包含用户的订单信息 | /odata/users?$Expand=Orders |
| 获取用户列表，包含用户的订单信息、订单明细 | /odata/users?$Expand=Orders($expand=OrderDetails) |

### 查询集合的总数 $count

OData 支持请求获取资源的计数。

| 描述 | 示例 |
| --- | --- |
| 获取用户列表的总数 | /odata/users?$count=true |

### 查询排序 $orderby

OData 支持请求获取排序的资源。

| 描述 | 示例 |
| --- | --- |
| 获取用户列表并根据用户名称排序 | /odata/users?$OrderBy=Name desc |

### 使用 Swagger 配置 OData

安装 NuGet 程序包

- `OData.Swagger`

在`ConfigureServices`中注册服务

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddOData();
    
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    services.AddSwaggerGen();
    
    // 使用 Swagger 配置 OData
    services.AddOdataSwaggerSupport();

    services.AddControllers();
}
```
