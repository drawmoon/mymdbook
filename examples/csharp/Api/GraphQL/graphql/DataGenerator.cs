using HttpApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpApi
{
    public static class DataGenerator
    {
        public static async Task InitSampleData(IServiceProvider serviceProvider, List<Order> sampleData)
        {
            using var dbContext = serviceProvider.GetService<AppDbContext>();

            await dbContext.Orders.AddRangeAsync(sampleData);

            await dbContext.SaveChangesAsync();
        }
    }
}
