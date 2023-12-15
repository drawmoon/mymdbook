using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace Flow.Core
{
    public class ScheduleWorkflowHost : IScheduleWorkflowHost
    {
        private readonly ConcurrentDictionary<string, IJobDetail> _jobs = new ConcurrentDictionary<string, IJobDetail>();
        private readonly IWorkflowHost _workflowHost;
        private readonly CancellationToken _cancellationToken;
        private readonly ISchedulerFactory _schedulerFactory = new StdSchedulerFactory();
        private IScheduler _scheduler;

        public ScheduleWorkflowHost(IWorkflowHost workflowHost, IHostApplicationLifetime applicationLifetime)
        {
            _workflowHost = workflowHost;
            _cancellationToken = applicationLifetime.ApplicationStopping;
        }

        public async Task StartAsync()
        {
            _scheduler = await _schedulerFactory.GetScheduler(_cancellationToken);
            _scheduler.JobFactory = new CusJobFactory(_workflowHost);

            await _scheduler.Start(_cancellationToken);
        }

        public async Task StopAsync()
        {
            await _scheduler.Shutdown(true, _cancellationToken);
        }

        public bool RegisterScheduleWorkflow<TWorkflow>(string workflowId) where TWorkflow : IWorkflow, new()
        {
            if (_scheduler == null)
            {
                return false;
            }

            _workflowHost.RegisterWorkflow<TWorkflow>();

            var job = JobBuilder.Create<ScheduleWorkflowJob>()
                .WithIdentity(new JobKey(workflowId, "ScheduleWorkflowGroup"))
                .Build();

            return _jobs.TryAdd(workflowId, job);
        }

        public async Task StartScheduleWorkflowAsync(string workflowId, string cronExpression)
        {
            if (_jobs.TryGetValue(workflowId, out var job))
            {
                var trigger = TriggerBuilder.Create()
                        .WithIdentity(new TriggerKey(workflowId, "ScheduleWorkflowGroup"))
                        .WithCronSchedule(cronExpression)
                        .Build();

                await _scheduler.ScheduleJob(job, trigger, _cancellationToken);
            }
        }

        public async Task<bool> StopScheduleWorkflowAsync(string workflowId)
        {
            return await _scheduler.DeleteJob(new JobKey(workflowId, "ScheduleWorkflowGroup"), _cancellationToken);
        }
    }
}
