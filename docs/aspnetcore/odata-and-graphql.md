# OData 与 GraphQL

- [OData](#odata)
- [GraphQL](#graphql)

## OData

> OData 定义了一组构建和使用 RESTful API 的最佳实践。 OData 可帮助您在构建 RESTful API 时专注于业务逻辑，而不必担心定义请求和响应头，状态代码，HTTP 方法，URL 约定，媒体类型，有效载荷格式，查询选项等的各种方法。OData 还提供跟踪更改，定义可重用过程的功能/动作以及发送异步/批处理请求的指南。

下面将创建一个基于 OData 的`ASP.NET Core API`项目，创建 OData 模型，使用 OData 的查询选项，例如`按需查询`、`扩展查询`、`过滤查询`等。

### 安装 Nuget 程序包

- `Microsoft.AspNetCore.OData`
- `Microsoft.OData.ModelBuilder`

### 创建实体和数据库上下文

新建一个用户实体和订单实体

```c#
public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

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
    }
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

OData 支持请求获取一个复杂对象中指定的属性，在接口传递`$select`参数实现。下面的请求是获取用户信息，但只需要获取`Id`和`Name`属性

```r
GET http://localhost:5000/odata/users?$Select=Id,Name

# 响应的数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users(Id,Name)",
  "value": [
    {
      "Id": 1,
      "Name": "xiaol"
    },
    {
      "Id": 2,
      "Name": "dalao"
    },
    {
      "Id": 3,
      "Name": "chonya"
    },
    {
      "Id": 4,
      "Name": "jinitaimei"
    }
  ]
}
```

### 过滤查询 $filter

OData 支持过滤请求的资源的集合，在接口传递`$filter`参数实现。OData 定义了一组逻辑运算符，结果为`true`或`false`，逻辑运算符不支持集合、实体和复杂类型的操作数。

#### Equals

判断左右表达式是否相等，相等返回`true`，否则返回`false`。下面的请求是过滤`LockoutEnabled = true`的用户

```r
GET http://localhost:5000/odata/users?$Filter=LockoutEnabled eq false

# 响应的数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
      "Id": 1,
      "Name": "xiaol",
      "Email": "xiaol@mail.com",
      "LockoutEnabled": false
    },
    {
      "Id": 2,
      "Name": "dalao",
      "Email": "dalao@mail.com",
      "LockoutEnabled": false
    },
    {
      "Id": 3,
      "Name": "chonya",
      "Email": "chonya@mail.com",
      "LockoutEnabled": false
    }
  ]
}
```

#### Not Equals

判断左右表达式是否不相等，不相等返回`true`，否则返回`false`。下面的请求是过滤`LockoutEnabled != true`的用户

```r
GET http://localhost:5000/odata/users?$Filter=LockoutEnabled ne true

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
      "Id": 1,
      "Name": "xiaol",
      "Email": "xiaol@mail.com",
      "LockoutEnabled": false
    },
    {
      "Id": 2,
      "Name": "dalao",
      "Email": "dalao@mail.com",
      "LockoutEnabled": false
    },
    {
      "Id": 3,
      "Name": "chonya",
      "Email": "chonya@mail.com",
      "LockoutEnabled": false
    }
  ]
}
```

#### Contains

判断某个属性的值是否包含指定字符串。下面的请求是过滤用户名包含`xiao`的用户

```r
GET http://localhost:5000/odata/users?$Filter=contains(Name, 'xiao')

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
      "Id": 1,
      "Name": "xiaol",
      "Email": "xiaol@mail.com",
      "LockoutEnabled": false
    }
  ]
}
```

#### And

左右表达式都必须满足条件，则`and`运算的结果为`true`，否则为`false`。下面是获取用户列表，并且`LockoutEnabled`等于`false`

```r
GET http://localhost:5000/odata/users?$Filter=Name eq 'xiaol' and LockoutEnabled eq false

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
        "Id": 1,
        "Name": "xiaol",
        "Email": "xiaol@mail.com",
        "LockoutEnabled": false
    }
  ]
}
```

#### Or

左右表达式任意一个满足条件，则`or`运算的结果为`true`，否则为`false`。下面是获取用户列表，或者`LockoutEnabled`等于`true`

```r
GET http://localhost:5000/odata/users?$Filter=Name eq 'xiaol' or LockoutEnabled eq true

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
        "Id": 1,
        "Name": "xiaol",
        "Email": "xiaol@mail.com",
        "LockoutEnabled": false
    },
    {
        "Id": 4,
        "Name": "jinitaimei",
        "Email": "jinitaimei@mail.com",
        "LockoutEnabled": true
    }
  ]
}
```

#### Not

取反，如果表达式返回的是`false`，则`not`运算返回为`true`。下面是获取用户名称不以`xiao`开头的用户列表

```r
GET http://localhost:5000/odata/users?$Filter=not startswith(Name, 'xiao')

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
        "Id": 2,
        "Name": "dalao",
        "Email": "dalao@mail.com",
        "LockoutEnabled": false
    },
    {
        "Id": 3,
        "Name": "chonya",
        "Email": "chonya@mail.com",
        "LockoutEnabled": false
    },
    {
        "Id": 4,
        "Name": "jinitaimei",
        "Email": "jinitaimei@mail.com",
        "LockoutEnabled": true
    }
  ]
}
```

#### In

```r
GET http://localhost:5000/odata/users?$Filter=Id in (1, 2)

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
        "Id": 1,
        "Name": "xiaol",
        "Email": "xiaol@mail.com",
        "LockoutEnabled": false
    },
    {
        "Id": 2,
        "Name": "dalao",
        "Email": "dalao@mail.com",
        "LockoutEnabled": false
    }
  ]
}
```

### 扩展查询 $expand

OData 支持检索资源包含的相关资源，在接口传递`$expand`参数实现。下面是请求获取用户列表，包含用户的订单信息

```r
GET http://localhost:5000/odata/users?$Expand=Orders

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users(Orders())",
  "value": [
    {
      "Id": 1,
      "Name": "xiaol",
      "Email": "xiaol@mail.com",
      "LockoutEnabled": false,
      "Orders": [
        {
          "Id": 1,
          "Name": "order1",
          "UserId": 1
        },
        {
          "Id": 2,
          "Name": "order2",
          "UserId": 1
        },
        {
          "Id": 3,
          "Name": "order3",
          "UserId": 1
        }
      ]
    },
    {
      "Id": 2,
      "Name": "dalao",
      "Email": "dalao@mail.com",
      "LockoutEnabled": false,
      "Orders": []
    },
    {
      "Id": 3,
      "Name": "chonya",
      "Email": "chonya@mail.com",
      "LockoutEnabled": false,
      "Orders": []
    },
    {
      "Id": 4,
      "Name": "jinitaimei",
      "Email": "jinitaimei@mail.com",
      "LockoutEnabled": true,
      "Orders": []
    }
  ]
}
```

### 查询集合的总数 $count

OData 支持请求获取资源的计数。下面是获取用户列表的总数

```r
http://localhost:5000/odata/users?$count=true

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "@odata.count": 4,
  "value": [
    {
      "Id": 1,
      "Name": "xiaol",
      "Email": "xiaol@mail.com",
      "LockoutEnabled": false
    },
    {
      "Id": 2,
      "Name": "dalao",
      "Email": "dalao@mail.com",
      "LockoutEnabled": false
    },
    {
      "Id": 3,
      "Name": "chonya",
      "Email": "chonya@mail.com",
      "LockoutEnabled": false
    },
    {
      "Id": 4,
      "Name": "jinitaimei",
      "Email": "jinitaimei@mail.com",
      "LockoutEnabled": true
    }
  ]
}
```

### 查询排序 $orderby

OData 支持请求获取排序的资源。下面是获取用户列表并根据用户名称排序

```r
GET http://localhost:5000/odata/users?$OrderBy=Name desc

# 响应数据
{
  "@odata.context": "http://localhost:5000/odata/$metadata#Users",
  "value": [
    {
        "Id": 1,
        "Name": "xiaol",
        "Email": "xiaol@mail.com",
        "LockoutEnabled": false
    },
    {
        "Id": 4,
        "Name": "jinitaimei",
        "Email": "jinitaimei@mail.com",
        "LockoutEnabled": true
    },
    {
        "Id": 2,
        "Name": "dalao",
        "Email": "dalao@mail.com",
        "LockoutEnabled": false
    },
    {
        "Id": 3,
        "Name": "chonya",
        "Email": "chonya@mail.com",
        "LockoutEnabled": false
    }
  ]
}
```
