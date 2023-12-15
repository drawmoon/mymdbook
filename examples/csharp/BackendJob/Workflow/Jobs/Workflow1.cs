using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flow.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Flow.Jobs
{
    public class Workflow1 : IWorkflow<MyDataClass>
    {
        public void Build(IWorkflowBuilder<MyDataClass> builder)
        {
            builder
                .StartWith(context =>
                {
                    Console.WriteLine("Starting workflow...");
                    return ExecutionResult.Next();
                })
                .If(data => data.Value1 != 0 && data.Value2 != 0)
                .Do(context =>
                {
                    context
                        .StartWith<AddNumber>()
                        .Input(step => step.Input1, data => data.Value1)
                        .Input(step => step.Input2, data => data.Value2)
                        .Output(data => data.Value3, step => step.Output)
                        .Then<ShowResult>()
                        .Input(step => step.Message, data => $"The answer is {data.Value3}");
                })
                .Then<DoSomething>();
        }

        public string Id => "workflow1";
        public int Version => 1;
    }
}
