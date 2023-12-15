var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    var proxyConfigDictionary = app.Configuration.GetSection("Proxy").Get<Dictionary<string, ProxyOptions>>();

    foreach (var (prefix, options) in proxyConfigDictionary)
    {
        app.Map(prefix, appBuilder =>
        {
            appBuilder.RunProxy(options);
        });
    }
}

app.MapGet("/", () =>
{
    return "Hello, World!";
});

app.Run();