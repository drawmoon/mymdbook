using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace Client.Services
{
    public interface IUsersWebApi : IHttpApi
    {
        [HttpGet("/api/users")]
        ITask<string[]> GetAllUser();

        [HttpGet("/api/users/{id}")]
        ITask<string> GetUser(int id);

        [HttpPost("/api/users")]
        ITask<string> AddUser([JsonContent] string json);

        [HttpPut("/api/users")]
        ITask<string> UpdateUser([JsonContent] string json);

        [HttpDelete("/api/users/{id}")]
        ITask<int> DeleteUser(int id);
    }
}
