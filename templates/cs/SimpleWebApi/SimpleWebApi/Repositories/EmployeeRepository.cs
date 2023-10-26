using SimpleWebApi.DbContexts;
using SimpleWebApi.Models.Management;
using SimpleWebApi.Repositories.Interfaces;

namespace SimpleWebApi.Repositories
{
    /// <summary>
    /// 员工信息仓储
    /// </summary>
    public class EmployeeRepository : RepositoryBase<ManagementDbContext, Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(ManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
