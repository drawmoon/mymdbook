using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Services.Interfaces;

namespace SimpleWebApi.Services.Extensions
{
    public static class ManagementApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddManagementApplicationServices(this IServiceCollection services)
        {
            // 添加用户身份的领域服务
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
