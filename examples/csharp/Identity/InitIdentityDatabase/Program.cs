using InitIdentityDatabase;
using InitIdentityDatabase.IdentityEntities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 注册数据库上下文
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(connectionString));

// 注册身份信息的管理服务，例如 UserManager, RoleManager
builder.Services.AddIdentity<UserIdentity, UserIdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

// 数据库迁移
using (var scope = app.Services.CreateScope())
{
    using (var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>())
    {
        dbContext.Database.Migrate();
    }
}

app.Run();