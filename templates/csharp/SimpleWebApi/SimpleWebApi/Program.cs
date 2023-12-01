using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using SimpleWebApi.DbContexts;
using SimpleWebApi.DbContexts.Extensions;
using SimpleWebApi.Repositories.Extensions;
using SimpleWebApi.Services.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 添加配置文件
builder.Configuration.AddJsonFile("sample_data.json");

// 配置 Furion
builder.Inject();
// 配置 Serilog
builder.WebHost.UseSerilogDefault(options =>
{
#if DEBUG
    options.MinimumLevel.Debug()
#else
    options.MinimumLevel.Information()
#endif
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .WriteTo.Console()
        .WriteTo.File("logs/application.log", LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, encoding: Encoding.UTF8);
});

// Add services to the container.

// 注册数据库上下文
builder.Services.AddDbContexts<ManagementDbContext>();

// 注册管理仓储服务
builder.Services.AddManagementRepositories();

// 注册管理服务
builder.Services.AddManagementApplicationServices();

builder.Services.AddControllers();

// 注册 Furion 服务
builder.Services.AddInject();

var app = builder.Build();

// Configure the HTTP request pipeline.

// 使用数据库迁移
app.EnsureDatabasesMigrated<ManagementDbContext>();
// 使用初始化管理员账号
await app.EnsureSeedSampleData();

app.MapControllers();

app.UseAuthorization();

// 使用 Furion 中间件，Swagger 的路由为 /api
app.UseInject("api");

await app.RunAsync();
