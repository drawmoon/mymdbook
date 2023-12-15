using HttpApi;
using HttpApi.Core;
using HttpApi.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("sample-data.json");

// 添加测试数据库
var databaseName = Guid.NewGuid().ToString();
builder.Services
    .AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<AppDbContext>((sp, options) => options.UseInMemoryDatabase(databaseName).UseInternalServiceProvider(sp));

builder.Services.AddHttpContextAccessor();

// 添加 GraphQL
builder.Services.AddGraphQLService();

// TODO: 修改 Read 方式
builder.Services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// 初始化示例数据
using (var scope = app.Services.CreateScope())
{
    await DataGenerator.InitSampleData(scope.ServiceProvider, app.Configuration.GetSection("SampleData").Get<List<Order>>());
}

// 启用 GraphQL
app.UseGraphQLService();

app.Run();