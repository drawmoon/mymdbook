using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceSample.Core
{
    public class MonitorLoop
    {
        private readonly IEnumerable<IBackgroundTask> _backgroudTasks;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly CancellationToken _stoppingToken;

        public MonitorLoop(IEnumerable<IBackgroundTask> backgroudTasks, IBackgroundTaskQueue backgroundTaskQueue, IHostApplicationLifetime hostApplicationLifetime)
        {
            _backgroudTasks = backgroudTasks;
            _backgroundTaskQueue = backgroundTaskQueue;
            _stoppingToken = hostApplicationLifetime.ApplicationStopping;

            Task.Run(() => Monitor());
        }

        private void Monitor()
        {
            while (!_stoppingToken.IsCancellationRequested)
            {
                foreach (var task in _backgroudTasks)
                {
                    _backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
                    {
                        await task.ExecuteAsync(token);

                        await Task.Delay(TimeSpan.FromSeconds(3));
                    });
                }
            }
        }
    }
}
