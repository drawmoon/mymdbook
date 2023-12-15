using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Flow.Core
{
    public interface IScheduleWorkflowHost
    {
        Task StartAsync();

        Task StopAsync();

        bool RegisterScheduleWorkflow<TWorkflow>(string workflowId) where TWorkflow : IWorkflow, new();

        Task StartScheduleWorkflowAsync(string workflowId, string cronExpression);

        Task<bool> StopScheduleWorkflowAsync(string workflowId);
    }
}
