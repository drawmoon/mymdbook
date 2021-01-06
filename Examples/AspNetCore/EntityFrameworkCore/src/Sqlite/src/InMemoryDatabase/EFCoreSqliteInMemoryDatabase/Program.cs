using EFCoreSqliteInMemoryDatabase.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EFCoreSqliteInMemoryDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(await CreateInMemoryDatabase())
                .Options;

            using var dbContext = new AppDbContext(options);

            await dbContext.Database.EnsureCreatedAsync();

            await Init(dbContext);

            var orgs = await dbContext.Organizations.ToListAsync();

            foreach (var org in orgs)
            {
                Console.WriteLine($"{org.Id}  {org.Name}");
            }
        }

        static async Task<SqliteConnection> CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            await connection.OpenAsync();

            return connection;
        }

        private static async Task Init(AppDbContext context)
        {
            await context.Organizations.AddRangeAsync(
                new Organization[]
                {
                    new Organization
                    {
                        Name = "航天信息公司"
                    },
                    new Organization
                    {
                        Name = "食品公司"
                    }
                });

            await context.SaveChangesAsync();
        }
    }
}
