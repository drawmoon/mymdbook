using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InMemoryDB.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace InMemoryDB
{
    public static class SampleDataGenerator
    {
        public static void InitSampleData(IServiceProvider serviceProvider)
        {
            using var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("./sample-data.json"));
            dbContext.AddRange(users);

            dbContext.SaveChanges();
        }
    }
}
