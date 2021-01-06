using ConsoleAppSample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ConsoleAppSample
{
    class Program
    {
        private static readonly DbContextOptions<AppDbContext> _options;

        static Program()
        {
            var databaseName = Guid.NewGuid().ToString();
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            await Init();

            using var context = new AppDbContext(_options);

            var orgs = await context.Organizations.ToListAsync();

            foreach (var org in orgs)
            {
                Console.WriteLine($"{org.Id}  {org.Name}");
            }
        }

        private static async Task Init()
        {
            using var context = new AppDbContext(_options);

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
