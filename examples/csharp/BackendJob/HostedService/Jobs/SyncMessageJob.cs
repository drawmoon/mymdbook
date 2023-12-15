using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QueuedHostedService.Core;

namespace QueuedHostedService.Tasks
{
    public class SyncMessageJob : IBackendJob
    {
        private readonly ILogger<SyncMessageJob> _logger;

        public SyncMessageJob(ILogger<SyncMessageJob> logger)
        {
            _logger = logger;
        }

        public Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                for (int i = 0; i < 5; i++)
                {
                    _logger.LogInformation("SyncMessageJob execute -> {0}", i);
                }
            }

            return Task.CompletedTask;
        }
    }
}
