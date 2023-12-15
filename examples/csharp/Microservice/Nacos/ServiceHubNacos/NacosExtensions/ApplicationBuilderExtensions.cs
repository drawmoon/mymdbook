using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NacosExtensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder TryAddServerFeatures(this IApplicationBuilder app, IConfiguration configuration)
        {
            var server = app.ApplicationServices.GetService<IServer>();

            var featureCollection = server.Features;

            var addresses = featureCollection.Get<IServerAddressesFeature>();

            if (addresses?.Addresses == null || addresses.Addresses.Count <= 0)
            {
                var serverAddressesFeature = (IServerAddressesFeature)new ServerAddressesFeature();

                serverAddressesFeature.Addresses.Add(GetUri(app, configuration));

                featureCollection.Set(serverAddressesFeature);
            }

            return app;
        }
        
        private static string GetUri(IApplicationBuilder app, IConfiguration configuration)
        {
            return $"http://{GetCurrentIp()}:{configuration["port"]}";
        }
        
        private static string GetCurrentIp()
        {
            var str = "127.0.0.1";
            try
            {
                var allNetworkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                var address = allNetworkInterfaces.Select(o => o.GetIPProperties()).SelectMany(o => o.UnicastAddresses)
                    .Where(o => o.Address.AddressFamily.ToString() == "InterNetwork" && !IPAddress.IsLoopback(o.Address)).Select(o => o.Address.ToString())
                    .FirstOrDefault();
                str = address ?? str;
            }
            catch
            {
                // ignored
            }

            return str;
        }
    }
}