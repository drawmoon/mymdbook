using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplicationSample.Models;

namespace WebApplicationSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add database.

            var databaseName = Guid.NewGuid().ToString();
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<AppDbContext>((sp, options) => options.UseInMemoryDatabase(databaseName).UseInternalServiceProvider(sp), ServiceLifetime.Singleton); 
            
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Init(app).GetAwaiter().GetResult();
        }

        private async Task Init(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<AppDbContext>();

            await context.Organizations.AddRangeAsync(
                new Organization[]
                {
                    new Organization
                    {
                        Id = Guid.NewGuid(),
                        Name = "航天信息公司"
                    },
                    new Organization
                    {
                        Id = Guid.NewGuid(),
                        Name = "食品公司"
                    }
                });

            await context.SaveChangesAsync();
        }
    }
}
