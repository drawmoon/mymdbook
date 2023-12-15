# WorkflowDemo

定时执行的工作流任务。

## Schedule Workflow

- DI

```csharp
services.AddSingleton<IScheduleWorkflowHost, ScheduleWorkflowHost>();
```

- Use IScheduleWorkflowHost

```csharp
var scheduleWorkflowHost = app.ApplicationServices.GetRequiredService<IScheduleWorkflowHost>();

scheduleWorkflowHost.StartAsync().Wait();

scheduleWorkflowHost.RegisterScheduleWorkflow<ScheduleWorkflow1>("ScheduleWorkflow1");

scheduleWorkflowHost.StartScheduleWorkflowAsync("ScheduleWorkflow1", "0/5 * * * * ?").Wait();

lifetime.ApplicationStopping.Register(() =>
{
    scheduleWorkflowHost.StopAsync().Wait();
});
```
