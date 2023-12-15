using Microsoft.AspNetCore.Mvc;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRefitClient<IUserApi>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri("http://localhost:8000");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", (IUserApi userApi) =>
{
    return userApi.GetAllUser();
});

app.MapGet("/{id}", (IUserApi userApi, [FromRoute] int id) =>
{
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