using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QueuedHostedService.Core;

namespace QueuedHostedService.Tasks
{
    public class SendMessageJob : IBackendJob
    {
        private readonly ILogger<SendMessageJob> _logger;

        public SendMessageJob(ILogger<SendMessageJob> logger)
        {
            _logger = logger;
        }

        public Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                for (int i = 0; i < 5; i++)
                {
                    _logger.LogInformation("SendMessageJob execute -> {0}", i);
                }
            }

            return Task.CompletedTask;
        }
    }
}
