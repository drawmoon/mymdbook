using HostedServiceSample.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceSample.Tasks
{
    public class BgTask2 : IBackgroundTask
    {
        private readonly ILogger<BgTask2> _logger;

        public BgTask2(ILogger<BgTask2> logger)
        {
            _logger = logger;
        }

        public Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                for (int i = 0; i < 5; i++)
                {
                    _logger.LogInformation($"simple2 {i}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
