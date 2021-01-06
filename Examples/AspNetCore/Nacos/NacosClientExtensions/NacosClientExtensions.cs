using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiClient;

namespace NacosClientExtensions
{
    public static class NacosClientExtensions
    {
        public static IHttpClientBuilder AddNacosTypedClient<TInterface>(this IServiceCollection services, Action<NacosHttpApiConfig, IServiceProvider> config)
             where TInterface : class, IHttpApi
        {
            return services.AddHttpClient(nameof(TInterface))
                .AddTypedClient<TInterface>((client, serviceProvider) =>
                {
                    var nacosHttpApiConfig = new NacosHttpApiConfig
                    {
                        ServiceProvider = serviceProvider
                    };

                    config.Invoke(nacosHttpApiConfig, serviceProvider);

                    return HttpApi.Create<TInterface>(nacosHttpApiConfig);
                });
        }
    }
}
