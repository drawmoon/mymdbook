using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace WebApiClientDemo.Services
{
    public interface IUsersWebApi : IHttpApi
    {
        [HttpGet("/api/1/users")]
        ITask<string[]> GetAllUser();

        [HttpGet("/api/1/users/{id}")]
        ITask<string> GetUser(int id);

        [HttpPost("/api/1/users")]
        ITask<string> AddUser([JsonContent] string json);

        [HttpPut("/api/1/users")]
        ITask<string> UpdateUser([JsonContent] string json);

        [HttpDelete("/api/1/users/{id}")]
        ITask<int> DeleteUser(int id);
    }
}
