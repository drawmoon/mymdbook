using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Flow.Core
{
    public class ScheduleWorkflowJob : IJob
    {
        private readonly IWorkflowHost _workflowHost;

        public ScheduleWorkflowJob(IWorkflowHost workflowHost)
        {
            _workflowHost = workflowHost;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _workflowHost.StartWorkflow(context.JobDetail.Key.Name);
        }
    }
}
