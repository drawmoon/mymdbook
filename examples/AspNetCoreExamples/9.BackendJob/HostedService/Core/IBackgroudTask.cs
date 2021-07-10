using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceDemo.Core
{
    public interface IBackgroudTask
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
