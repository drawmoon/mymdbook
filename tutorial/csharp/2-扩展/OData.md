# Hey OData

> OData 定义了一组构建和使用 RESTful API 的最佳实践。 OData 可帮助您在构建 RESTful API 时专注于业务逻辑，而不必担心定义请求和响应头，状态代码，HTTP 方法，URL 约定，媒体类型，有效载荷格式，查询选项等的各种方法。OData 还提供跟踪更改，定义可重用过程的功能/动作以及发送异步/批处理请求的指南。

下面将创建一个基于 OData 的 `ASP.NET Core Web API` 项目，.NET 版本 `6.0`。

## 安装

```bash
Install-Package Microsoft.AspNetCore.OData
Install-Package Microsoft.OData.ModelBuilder
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.InMemory
```

## 创建实体和数据库上下文

新建一个用户实体和订单实体

```csharp
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

新建 `AppDbContext.cs` 文件，创建数据库上下文

```csharp
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

注册数据库上下文到服务容器中

```csharp
// 添加 in-memory 数据库
var databaseName = Guid.NewGuid().ToString();
builder.Services
    .AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<AppDbContext>((sp, options) => options.UseInMemoryDatabase(databaseName).UseInternalServiceProvider(sp));
```

## 创建 OData 模型

新建一个 `AppEdmModel.cs` 文件，将实体添加 OData 模型中。 \
在 `AppEdmModel` 类中可以指定实体的 CRUD 操作权限，权限声明添加到 OData 模型中后，授权中间件将会读取这些权限声明，处理不同请求所需要的权限范围。

```csharp
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

## 注册 OData 服务

注册 OData 服务，和指定 OData 启用的查询选项

```csharp
builder.Services.AddControllers()
    .AddOData(options => options.Select().Expand().Filter().OrderBy().SetMaxTop(100).Count());
```

## 创建控制器

新建控制器，并继承 `ODataController` 类

```csharp
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

## 查询选项

### 按需查询

OData 支持通过 `$select` 请求获取一个复杂对象中指定的属性。

示例

```
# 获取用户信息，返回结果只包含 Id 和 Name 字段
/odata/users?$Select=Id,Name
```

### 过滤查询

OData 支持通过 `$filter` 过滤请求的资源的集合。OData 定义了一组逻辑运算符，结果为 `true` 或 `false`，逻辑运算符不支持集合、实体和复杂类型的操作数。

- `eq`: 判断左右表达式是否相等
- `ne`: 判断左右表达式是否不相等
- `contains`: 判断某个属性的值是否包含指定字符串
- `gt`: 大于
- `ge`: 大于或等于
- `lt`: 小于
- `le`: 小于或等于
- `and`: 左右表达式都必须满足条件
- `or`: 左右表达式任意一个满足条件
- `not`: 取反
- `in`: 判断是否存在指定的集合中

示例

```sh
# 查询 LockoutEnabled 等于 true 的用户列表
/odata/users?$Filter=LockoutEnabled eq false

# 查询 LockoutEnabled 不等于 true 的用户列表
/odata/users?$Filter=LockoutEnabled ne true

# 查询用户名包含 xiao 的用户列表
/odata/users?$Filter=contains(Name, 'xiao')

# 查询年龄大于 20 的用户列表
/odata/users?$filter=Age gt 20

# 查询年龄大于或等于 20 的用户列表
/odata/users?$filter=Age ge 20

# 查询年龄小于 20 的用户列表
/odata/users?$filter=Age lt 20

# 查询年龄小于或等于 20 的用户列表
/odata/users?$filter=Age le 20

# 查询用户名等于 xiaol，并且 LockoutEnabled 等于 false 的用户列表
/odata/users?$Filter=Name eq 'xiaol' and LockoutEnabled eq false

# 查询用户名等于 xiaol，或者用户名等于 chonya 的用户列表
/odata/users?$Filter=Name eq 'xiaol' or Name eq 'chonya'

# 查询用户名不以 xiao 开头的用户列表
/odata/users?$Filter=not startswith(Name, 'xiao')

# 查询用户名等于 xiaol、chonya 的用户列表
/odata/users?$Filter=Name in ('xiaol', 'chonya')
```

### 扩展查询

OData 支持通过 `$expand` 检索资源包含的相关资源。

示例

```sh
# 获取用户列表，包含用户的订单信息
/odata/users?$Expand=Orders

# 获取用户列表，包含用户的订单信息、订单明细
/odata/users?$Expand=Orders($expand=OrderDetails)
```

### 查询集合的总数

OData 支持请求获取资源的计数。

示例

```sh
# 获取用户列表的总数
/odata/users?$count=true
```

### 查询分页

OData 支持通过组合 `$top` 和 `$skip` 请求获取资源。

示例

```sh
# 获取用户列表第一页的用户，每页显示 5 条数据
/odata/users?$top=5&$skip=0
```

### 查询排序

OData 支持请求获取排序的资源。

示例

```sh
# 获取用户列表并根据用户名称排序
/odata/users?$OrderBy=Name desc
```
