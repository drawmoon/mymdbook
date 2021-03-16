using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Discovery.Client;
using WebApiClient;
using WebApiClient.Extensions.DiscoveryClient;
using WebApiClient.Extensions.HttpClientFactory;

namespace Client
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
            #region Spring-Cloud
            if (Configuration.GetValue<bool>("UseSpringCloud"))
            {
                services.AddDiscoveryClient(Configuration.GetSection("SpringCloud"));
            }
            #endregion

            #region ValueWebApi
            if (Configuration.GetValue<bool>("UseSpringCloud"))
            {
                services.AddDiscoveryTypedClient<IUsersWebApi>(options =>
                {
                    options.HttpHost = new Uri("http://ServerPpL7XK");
                    options.FormatOptions.DateTimeFormat = DateTimeFormats.ISO8601_WithoutMillisecond;
                })
                .ConfigureHttpClient(SetDefaultRequestHeaders);
            }
            else
            {
                services.AddHttpApiTypedClient<IUsersWebApi>(options =>
                {
                    options.HttpHost = new Uri("http://localhost:1903");
                    options.FormatOptions.DateTimeFormat = DateTimeFormats.ISO8601_WithoutMillisecond;
                })
                .ConfigureHttpClient(SetDefaultRequestHeaders);
            }
            #endregion

            services.AddHttpContextAccessor();

            services.AddControllers();
        }

        // 设置HttpClient默认请求头
        private void SetDefaultRequestHeaders(IServiceProvider serviceProvider, HttpClient client)
        {
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authorization))
            {
                client.DefaultRequestHeaders.Add("Authorization", authorization.ToString());
            }
            if (httpContext.Request.Headers.TryGetValue("Cookie", out var cookie))
            {
                client.DefaultRequestHeaders.Add("Cookie", cookie.ToString());
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Spring-Cloud
            if (Configuration.GetValue<bool>("UseSpringCloud"))
            {
                app.UseDiscoveryClient();
            }
            #endregion
        }
    }
}
