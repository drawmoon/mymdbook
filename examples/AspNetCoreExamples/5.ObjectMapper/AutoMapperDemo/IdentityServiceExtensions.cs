using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapperDemo.Mappers;

namespace AutoMapperDemo
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityMapping(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationProvider>(sp => new MapperConfiguration(config =>
            {
                config.AddProfile(typeof(IdentityMapperProfile));
            }));

            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            return services;
        }
    }
}
