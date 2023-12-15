using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QueuedHostedService.Core
{
    public class MonitorLoop
    {
        private readonly IEnumerable<IBackendJob> _backendJobs;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly CancellationToken _stoppingToken;

        public MonitorLoop(IEnumerable<IBackendJob> backendJobs, IBackgroundTaskQueue backgroundTaskQueue, IHostApplicationLifetime hostApplicationLifetime)
        {
            _backendJobs = backendJobs;
            _backgroundTaskQueue = backgroundTaskQueue;
            _stoppingToken = hostApplicationLifetime.ApplicationStopping;

            Task.Run(() => Monitor());
        }

        private void Monitor()
        {
            while (!_stoppingToken.IsCancellationRequested)
            {
                foreach (var task in _backendJobs)
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
