using GraphQLDemo.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace GraphQLDemo
{
    public static class DataGenerator
    {
        public static void InitSampleData(IServiceProvider serviceProvider)
        {
            using var dbContext = serviceProvider.GetService<AppDbContext>();

            dbContext.Orders.AddRange(new List<Order>
            {
                new Order
                {
                    Name = "order1",
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            Name = "detail1",
                        },
                        new OrderDetail
                        {
                            Name = "detail2",
                        }
                    }
                },
                new Order
                {
                    Name = "order2",
                },
                new Order
                {
                    Name = "order3",
                }
            });

            dbContext.SaveChanges();
        }
    }
}
