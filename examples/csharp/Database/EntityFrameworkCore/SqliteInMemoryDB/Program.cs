using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using SqliteInMemoryDB;
using SqliteInMemoryDB.Models;


var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(CreateInMemoryDatabase())
    .Options;

using (AppDbContext dbContext = new(options))
{
    dbContext.Database.EnsureCreated();

    SampleDataGenerator.InitSampleData(dbContext);

    foreach (var user in dbContext.Users)
    {
        Console.WriteLine($"{user.Id}  {user.Name}");
    }
}


static SqliteConnection CreateInMemoryDatabase()
{
    SqliteConnection connection = new("Filename=:memory:");

    connection.Open();

    return connection;
}
