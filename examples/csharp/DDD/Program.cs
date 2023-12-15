using DDDExample.Admin.Service;
using DDDExample.Domain.User.Factory;
using DDDExample.Domain.User.Repository;
using DDDExample.Domain.User.Service;
using DDDExample.EntityFrameworkCore;
using DDDExample.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var databaseName = Guid.NewGuid().ToString();
builder.Services
    .AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFactory, UserFactory>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();