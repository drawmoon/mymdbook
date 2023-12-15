var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    // JsonPatch 依赖 NewtonsoftJson, 需要引用 Microsoft.AspNetCore.Mvc.NewtonsoftJson;
    .AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();