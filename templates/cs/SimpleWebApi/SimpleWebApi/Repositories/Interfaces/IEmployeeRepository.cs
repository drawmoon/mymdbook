using SimpleWebApi.DbContexts;
using SimpleWebApi.Models.Management;

namespace SimpleWebApi.Repositories.Interfaces
{
    /// <summary>
    /// 员工信息仓储接口
    /// </summary>
    public interface IEmployeeRepository : IRepositoryBase<ManagementDbContext, Employee, int>
    {
    }
}
