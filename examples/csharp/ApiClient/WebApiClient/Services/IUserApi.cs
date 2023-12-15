using WebApiClient;
using WebApiClient.Attributes;

public interface IUserApi : IHttpApi
{
    [HttpGet("/users")]
    ITask<string[]> GetAllUser();

    [HttpGet("/users/{id}")]
    ITask<string> GetUser(int id);

    [HttpPost("/users")]
    ITask<string> AddUser([JsonContent] string user);

    [HttpPut("/users")]
    ITask<string> UpdateUser([JsonContent] string user);

    [HttpDelete("/users/{id}")]
    ITask<int> DeleteUser(int id);
}