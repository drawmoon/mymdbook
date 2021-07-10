using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowDemo.Services;

namespace WorkflowDemo.Jobs
{
    public class DoSomething : StepBody
    {
        private readonly IMyService _myService;

        public DoSomething(IMyService myService)
        {
            _myService = myService;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            _myService.DoTheThings();
            return ExecutionResult.Next();
        }
    }
}
