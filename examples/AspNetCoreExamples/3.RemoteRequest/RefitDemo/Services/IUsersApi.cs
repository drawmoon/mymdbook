using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefitDemo.Services
{
    interface IUsersApi
    {
        [Get("/api/1/users")]
        Task<string[]> GetAllUser();

        [Get("/api/1/users/{id}")]
        Task<string> GetUser(int id);
    }
}
