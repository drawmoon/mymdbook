using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebApiApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateDefaultHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .Inject()
                        .UseStartup<Startup>();
                });

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables("DOTNET_");
                    if (args == null)
                        return;
                    config.AddCommandLine(args);
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile("appsettings.Development.json", true, true);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

                    // IMPORTANT: This needs to be added *before* configuration is loaded, this lets
                    // the defaults be overridden by the configuration.
                    if (isWindows)
                    {
                        // Default the EventLogLoggerProvider to warning or above
                        logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
                    }

                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();

                    if (isWindows)
                    {
                        // Add the EventLogLoggerProvider on windows machines
                        logging.AddEventLog();
                    }

                    logging.Configure(options =>
                    {
                        options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                            | ActivityTrackingOptions.TraceId
                                                            | ActivityTrackingOptions.ParentId;
                    });

                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    var flag = context.HostingEnvironment.IsDevelopment();
                    options.ValidateScopes = flag;
                    options.ValidateOnBuild = flag;
                })
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(options =>
                        {
                            if (!IPAddress.TryParse("0.0.0.0", out var address))
                            {
                                var addresses = Dns.GetHostAddresses(Dns.GetHostName());
                                var ipv4 = addresses.First(t => t.AddressFamily == AddressFamily.InterNetwork);
                                address = ipv4;
                            }

                            options.Listen(address, 5000);
                        })
                        .Inject()
                        .UseStartup<Startup>();
                });
        }
    }
}
