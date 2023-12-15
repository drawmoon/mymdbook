using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QueuedHostedService.Core
{
    public interface IBackendJob
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
