using Microsoft.Extensions.DependencyInjection;
using Nacos.AspNetCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;

namespace NacosClientExtensions
{
    public class NacosHttpApiConfig : HttpApiConfig
    {
        private string _serviceName;

        public string ServiceName
        {
            get { return _serviceName; }
            set
            {
                _serviceName = value;
                if (string.IsNullOrWhiteSpace(_serviceName)) return;
                var nacosServerManager = ServiceProvider.GetRequiredService<INacosServerManager>();
                HttpHost = new Uri(nacosServerManager.GetServerAsync(_serviceName).GetAwaiter().GetResult());
            }
        }

    }
}
