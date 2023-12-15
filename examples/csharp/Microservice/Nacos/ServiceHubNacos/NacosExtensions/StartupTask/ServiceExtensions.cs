using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NacosExtensions.StartupTask
{
    public static class ServiceExtensions
    {
        public static IHost RunStartupTask(this IHost host)
        {
            foreach (var startupTask in host.Services.GetServices<IStartupTask>())
            {
                startupTask.Execute();
            }

            return host;
        }
    }
}