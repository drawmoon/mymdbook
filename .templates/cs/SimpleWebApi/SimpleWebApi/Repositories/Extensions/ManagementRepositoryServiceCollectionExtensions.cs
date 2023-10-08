using Microsoft.Extensions.DependencyInjection;
using SimpleWebApi.Repositories.Interfaces;

namespace SimpleWebApi.Repositories.Extensions
{
    public static class ManagementRepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddManagementRepositories(this IServiceCollection services)
        {
            // 注册仓储服务
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
