using System.Linq;

namespace GlobalServiceProvider;

public static class App
{
    public static IConfiguration Configuration;

    public static IServiceCollection ServiceCollection;

    public static IServiceProvider ServiceProvider;

    public static TService GetRequiredService<TService>() where TService : notnull
    {
        var serviceType = typeof(TService);
        
        // 判断服务是否为单例
        if (ServiceCollection.Any(x => x.ServiceType == (serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : serviceType) && x.Lifetime == ServiceLifetime.Singleton))
        {
            return (TService)ServiceProvider.GetRequiredService(serviceType);
        }

        // 创建新的作用域
        using var serviceScope = ServiceProvider.CreateScope();
        return (TService)serviceScope.ServiceProvider.GetRequiredService(serviceType);
    }
}