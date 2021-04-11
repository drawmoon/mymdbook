using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services
{
    interface IUsersApi
    {
        [Get("/api/users")]
        Task<string[]> GetAllUser();

        [Get("/api/users/{id}")]
        Task<string> GetUser(int id);
    }
}
