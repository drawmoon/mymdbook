using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QueuedHostedService.Core
{
    public class QueuedHostedService : BackgroundService
    {
        public QueuedHostedService(IBackgroundTaskQueue backgroundTaskQueue)
        {
            BackgroundTaskQueue = backgroundTaskQueue;
        }

        public IBackgroundTaskQueue BackgroundTaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await BackgroundTaskQueue.DequeueAsync(stoppingToken);

                await workItem.Invoke(stoppingToken);
            }
        }
    }
}
