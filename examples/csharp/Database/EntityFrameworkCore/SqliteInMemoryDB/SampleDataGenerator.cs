using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SqliteInMemoryDB.Models;

namespace SqliteInMemoryDB
{
    public static class SampleDataGenerator
    {
        public static void InitSampleData(AppDbContext dbContext)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("./sample-data.json"));
            
            dbContext.AddRange(users);

            dbContext.SaveChanges();
        }
    }
}
