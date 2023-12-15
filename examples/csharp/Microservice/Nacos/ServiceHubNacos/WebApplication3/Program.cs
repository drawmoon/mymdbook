using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.Microsoft.Extensions.Configuration;
using NacosExtensions.Configuration;
using NacosExtensions.StartupTask;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .RunStartupTask() // 运行启动任务
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    IHostEnvironment hostingEnvironment = hostingContext.HostingEnvironment;
                    
                    // 添加ini配置文件
                    config.AddIniFile("appsettings.ini", true, true).AddIniFile("appsettings." + hostingEnvironment.EnvironmentName + ".ini", true, true);
                    
                    if (hostingEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostingEnvironment.ApplicationName))
                    {
                        Assembly assembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                        
                        if (assembly != (Assembly) null)
                            config.AddUserSecrets(assembly, true);
                    }
                    
                    config.AddEnvironmentVariables();
                    
                    if (args == null)
                        return;
                    
                    config.AddCommandLine(args);

                    // 添加Nacos配置管理服务
                    config.AddNacosConfiguration(source =>
                    {
                        var configurationRoot = config.Build();

                        var configuration = configurationRoot.GetSection("NacosConfig");
                    
                        configuration.Bind(source);
                        
                        // ini配置文件的解析器
                        source.NacosConfigurationParser = new IniConfigurationParser();
                    });
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}