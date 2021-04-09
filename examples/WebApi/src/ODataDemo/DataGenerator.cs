using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ODataDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ODataDemo
{
    public static class DataGenerator
    {
        public static void InitSampleData(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IWebHostEnvironment>();
            using var dbContext = serviceProvider.GetService<AppDbContext>();
            var text = File.ReadAllText(Path.Combine(env.ContentRootPath, "sample-data.json"));
            var users = JsonConvert.DeserializeObject<List<User>>(text);
            dbContext.AddRange(users);
            dbContext.SaveChanges();
        }
    }
}
