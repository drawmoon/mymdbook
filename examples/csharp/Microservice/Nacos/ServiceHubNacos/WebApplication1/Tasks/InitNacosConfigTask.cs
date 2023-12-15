using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Nacos;
using Nacos.Microsoft.Extensions.Configuration;
using NacosExtensions.StartupTask;

namespace WebApplication1.Tasks
{
    public class InitNacosConfigTask : IStartupTask
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly INacosConfigClient _nacosConfigClient;
        private readonly NacosConfigurationSource _nacosConfigurationSource = new NacosConfigurationSource();

        public InitNacosConfigTask(IHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _hostEnvironment = hostEnvironment;

            var nacosConfig = (IConfiguration) configuration.GetSection("NacosConfig");
            nacosConfig.Bind(_nacosConfigurationSource);

            _nacosConfigClient = new NacosMsConfigClient(NullLoggerFactory.Instance, new NacosOptions
            {
                ServerAddresses = _nacosConfigurationSource.ServerAddresses,
                Namespace = _nacosConfigurationSource.Tenant,
                AccessKey = _nacosConfigurationSource.AccessKey,
                ClusterName = _nacosConfigurationSource.ClusterName,
                ContextPath = _nacosConfigurationSource.ContextPath,
                EndPoint = _nacosConfigurationSource.EndPoint,
                DefaultTimeOut = _nacosConfigurationSource.DefaultTimeOut,
                SecretKey = _nacosConfigurationSource.SecretKey,
                Password = _nacosConfigurationSource.Password,
                UserName = _nacosConfigurationSource.UserName,
                ListenInterval = 5000
            });
        }

        public void Execute()
        {
            var configContent = _nacosConfigClient.GetConfigAsync(new GetConfigRequest
            {
                DataId = _nacosConfigurationSource.DataId,
                Group = _nacosConfigurationSource.Group,
                Tenant = _nacosConfigurationSource.Tenant
            }).ConfigureAwait(false).GetAwaiter().GetResult();
            
            if (configContent == null)
            {
                var filename = "appsettings";
                
                if (_hostEnvironment.IsDevelopment())
                {
                    filename += $".{_hostEnvironment.EnvironmentName}";
                }

                var content = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, $"{filename}.json"));
                
                _nacosConfigClient.PublishConfigAsync(new PublishConfigRequest
                {
                    DataId = _nacosConfigurationSource.DataId,
                    Group = _nacosConfigurationSource.Group,
                    Tenant = _nacosConfigurationSource.Tenant,
                    Content = content,
                    Type = "json"
                }).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }
    }
}