using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Core;
using Flow.Models;
using Flow.ScheduleWorkflows;
using Flow.Services;
using Flow.Workflows;
using Flow.Workflows.Steps;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowCore.Interface;

namespace Flow
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWorkflow();

            #region Workflow simple
            services.AddTransient<IMyService, MyService>();
            services.AddTransient<DoSomething>();
            #endregion

            #region ScheduleWorkflow simple
            services.AddSingleton<IScheduleWorkflowHost, ScheduleWorkflowHost>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            #region Workflow simple
            var workflowHost = app.ApplicationServices.GetRequiredService<IWorkflowHost>();

            workflowHost.RegisterWorkflow<Workflow1, MyDataClass>();

            workflowHost.Start();

            var myDataClass = new MyDataClass
            {
                Value1 = 1,
                Value2 = 2
            };

            workflowHost.StartWorkflow("Workflow1", myDataClass).Wait();

            //lifetime.ApplicationStopping.Register(() =>
            //{
            //    workflowHost.Stop();
            //});
            workflowHost.Stop();
            #endregion

            #region ScheduleWorkflow simple
            var scheduleWorkflowHost = app.ApplicationServices.GetRequiredService<IScheduleWorkflowHost>();

            scheduleWorkflowHost.StartAsync().Wait();

            scheduleWorkflowHost.RegisterScheduleWorkflow<ScheduleWorkflow1>("ScheduleWorkflow1");

            scheduleWorkflowHost.StartScheduleWorkflowAsync("ScheduleWorkflow1", "0/5 * * * * ?").Wait();

            lifetime.ApplicationStopping.Register(() =>
            {
                scheduleWorkflowHost.StopAsync().Wait();
            });
            #endregion
        }
    }
}
