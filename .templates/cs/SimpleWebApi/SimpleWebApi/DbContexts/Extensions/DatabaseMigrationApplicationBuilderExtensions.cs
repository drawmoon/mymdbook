using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleWebApi.Configuration.Configurations;
using SimpleWebApi.Models.Management;
using SimpleWebApi.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.DbContexts.Extensions
{
    public static class DatabaseMigrationApplicationBuilderExtensions
    {
        public static IApplicationBuilder EnsureDatabasesMigrated<TManagementDbContext>(this IApplicationBuilder app)
            where TManagementDbContext : DbContext
        {
            // 在此处实现数据库迁移
            // 示例中使用基于内存的数据库无法使用迁移
            // using (var dbContext = App.GetRequiredService<TManagementDbContext>())
            // {
            //     dbContext.Database.Migrate();
            // }

            return app;
        }

        public static async Task<IApplicationBuilder> EnsureSeedSampleData(this IApplicationBuilder app)
        {
            var sampleData = App.Configuration.GetSection(nameof(SampleData)).Get<SampleData>();

            if (sampleData != null)
            {
                var employeeRepository = App.GetRequiredService<IEmployeeRepository>();

                // 初始员工数据
                foreach (var employee in sampleData.Employees)
                {
                    var dbEmployee = await employeeRepository.Entities.Where(e => e.Name == employee.Name).FirstOrDefaultAsync();
                    if (dbEmployee == null)
                    {
                        await employeeRepository.InsertAsync(new Employee
                        {
                            Name = employee.Name,
                            Age = employee.Age
                        });
                    }
                }

                await employeeRepository.SaveChangesAsync();
            }

            return app;
        }
    }
}
