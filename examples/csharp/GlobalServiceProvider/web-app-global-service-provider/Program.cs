using GlobalServiceProvider;
using GlobalServiceProvider.Middlewares;
using GlobalServiceProvider.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("values.json");

builder.WebHost.ConfigureServices((context, services) =>
{
    App.Configuration = context.Configuration;
    App.ServiceCollection = services;

    services.AddTransient<IStartupFilter, StartupFilter>();
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<ITransientService, TransientService>();
builder.Services.AddScoped<IScopedService, ScopedService>();
builder.Services.AddSingleton<ISingletonService, SingletonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
