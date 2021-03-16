using HostedServiceSample.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceSample.Tasks
{
    public class BgTask1 : IBackgroundTask
    {
        private readonly ILogger<BgTask1> _logger;

        public BgTask1(ILogger<BgTask1> logger)
        {
            _logger = logger;
        }

        public Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                for (int i = 0; i < 5; i++)
                {
                    _logger.LogInformation($"simple1 {i}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
