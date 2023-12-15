using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Flow.Core
{
    public class CusJobFactory : IJobFactory
    {
        private readonly IWorkflowHost _workflowHost;

        public CusJobFactory(IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            _workflowHost.Start();

            return new ScheduleWorkflowJob(_workflowHost);
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();

            _workflowHost.Stop();
        }
    }
}
