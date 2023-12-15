using ApiDocument;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder();

// Add services to the container.

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseReDoc(options =>
{
    options.RoutePrefix = "api-docs";

    options.DocumentTitle = "API Doc";
    options.SpecUrl = "/swagger/v1/swagger.json";
});

app.MapControllers();

app.Run();
