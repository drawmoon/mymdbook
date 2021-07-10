using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiApplication.Exceptions.ResultProviders;

namespace WebApiApplication
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
            services.AddControllers()
            #region Furion
                .AddInjectWithUnifyResult<RESTfulResultProvider>() // RESTful 风格的规范化结果
                // .AddInjectWithUnifyResult<ProblemJsonResultProvider>() // Problem+Json 风格的规范化结果
                // .AddDynamicApiControllers() // 动态接口，默认已在AddInject中注册
                // .AddFriendlyException() // 异常处理，默认已在AddInject中注册
                // .AddUnifyResult<RESTfulResultProvider>() // 规范化结果服务，默认已在AddInjectWithUnifyResult中注册
                ;
            #endregion


            #region Furion
            //services.AddAuthorization();

            //services.AddMvcCore()
            //    .AddApiExplorer();

            //services.AddInject(); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUnifyResultStatusCodes();

            app.UseRouting();

            app.UseAuthorization();

            #region Furion
            app.UseInject("api-docs"); 
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
