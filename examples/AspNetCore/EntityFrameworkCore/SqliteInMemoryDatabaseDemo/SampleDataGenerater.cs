using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SqliteInMemoryDatabaseDemo.Models;

namespace SqliteInMemoryDatabaseDemo
{
    public static class SampleDataGenerater
    {
        public static void InitSampleData(AppDbContext dbContext)
        {
            dbContext.AddRange(new List<User>
            {
                new User
                {
                    Name = "user1"
                },
                new User
                {
                    Name = "user2"
                }
            });

            dbContext.SaveChanges();
        }
    }
}
