using Microsoft.AspNetCore.Mvc;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Spring Cloud 相关配置
//if (builder.Configuration.GetValue<bool>("UseSpringCloud"))
//{
//    builder.Services.AddDiscoveryClient(builder.Configuration.GetSection("SpringCloud"));
//}

builder.Services.AddApiClient<IUserApi>(options =>
{
    options.HttpHost = new Uri("http://localhost:8000");
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Spring Cloud 相关配置
//if (app.Configuration.GetValue<bool>("UseSpringCloud"))
//{
//    app.UseDiscoveryClient();
//}

app.MapGet("/", async (IUserApi userApi) =>
{
    return await userApi.GetAllUser();
});

app.MapGet("/{id}", async (IUserApi userApi, [FromRoute] int id) =>
{
    return await userApi.GetUser(id);
});

app.Run();