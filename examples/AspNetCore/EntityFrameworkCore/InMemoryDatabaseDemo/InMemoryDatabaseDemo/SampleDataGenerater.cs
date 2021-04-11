using InMemoryDatabaseDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace InMemoryDatabaseDemo
{
    public static class SampleDataGenerater
    {
        public static void InitSampleData(IServiceProvider serviceProvider)
        {
            using var dbContext = serviceProvider.GetService<AppDbContext>();

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
