using SimpleWebApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleWebApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedList<EmployeeDTO>> GetEmployees(int page = 1, int pageSize = 10);
    }
}
