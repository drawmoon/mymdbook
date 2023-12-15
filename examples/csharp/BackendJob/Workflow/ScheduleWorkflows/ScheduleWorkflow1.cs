using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Flow.ScheduleWorkflows
{
    public class ScheduleWorkflow1 : IWorkflow
    {
        public void Build(IWorkflowBuilder<object> builder)
        {
            builder.StartWith(context =>
            {
                Console.WriteLine("Doing stuff...");
                return ExecutionResult.Next();
            });
        }

        public string Id => "ScheduleWorkflow1";
        public int Version => 1;
    }
}
