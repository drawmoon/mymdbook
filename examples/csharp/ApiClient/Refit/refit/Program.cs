using Microsoft.AspNetCore.Mvc;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () =>
{
    var userApi = RestService.For<IUserApi>("http://localhost:8000");

    return userApi.GetAllUser();
});

app.MapGet("/{id}", ([FromRoute] int id) =>
{
    var userApi = RestService.For<IUserApi>("http://localhost:8000");

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