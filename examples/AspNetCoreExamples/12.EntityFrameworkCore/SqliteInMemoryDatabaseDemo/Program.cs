using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SqliteInMemoryDatabaseDemo.Models;
using System;
using System.Threading.Tasks;

namespace SqliteInMemoryDatabaseDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;

            using AppDbContext dbContext = new(options);

            dbContext.Database.EnsureCreated();

            SampleDataGenerater.InitSampleData(dbContext);

            foreach (var user in dbContext.Users)
            {
                Console.WriteLine($"{user.Id}  {user.Name}");
            }
        }

        private static SqliteConnection CreateInMemoryDatabase()
        {
            SqliteConnection connection = new("Filename=:memory:");

            connection.Open();

            return connection;
        }
    }
}
