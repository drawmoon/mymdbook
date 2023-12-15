using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace WebApplication2.Services.Interfaces
{
    public interface IValuesApi : IHttpApi
    {
        [HttpGet("api/values")]
        ITask<string[]> Get();
    }
}
