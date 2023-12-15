var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Add services to the container.

// Configure the HTTP request pipeline.

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();

app.Run();
