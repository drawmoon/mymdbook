using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleWebApi.DbContexts.Extensions
{
    public static class DatabaseServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts<TManagementDbContext>(this IServiceCollection services)
            where TManagementDbContext : DbContext
        {
            // 添加数据库上下文，示例项目使用的是基于内存的数据库
            // 这里也可以根据配置的数据库类型实现多数据库的支持
            var databaseName = Guid.NewGuid().ToString();
            services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<TManagementDbContext>((sp, options) => options.UseInMemoryDatabase(databaseName).UseInternalServiceProvider(sp));

            return services;
        }
    }
}
