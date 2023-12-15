using Microsoft.AspNetCore.Mvc;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient("user", client =>
{
    client.BaseAddress = new Uri("http://localhost:8000");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient("user");
    var userApi = RestService.For<IUserApi>(client);

    return userApi.GetAllUser();
});

app.MapGet("/{id}", (IHttpClientFactory factory, [FromRoute] int id) =>
{
    var client = factory.CreateClient("user");
    var userApi = RestService.For<IUserApi>(client);

    return userApi.GetUser(id);
});

app.Run();


interface IUserApi
{
    [Get("/users")]
    Task<string[]> GetAllUser();

    [Get("/users/{id}")]
    Task<string> GetUser(int id);
}