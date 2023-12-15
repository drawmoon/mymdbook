var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/api/users", () =>
{
    return new[] { "value1", "value2" };
});

app.MapPost("/api/users", () =>
{
    return true;
});

app.Run();