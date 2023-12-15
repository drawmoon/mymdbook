using HttpApi.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// 添加 GraphQL
builder.Services.AddGraphQLService();

// TODO: 修改 Read 方式
builder.Services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// 启用 GraphQL
app.UseGraphQLService();

app.Run();