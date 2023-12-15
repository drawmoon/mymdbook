using System.Collections.Generic;
using System.Security.Claims;

namespace HttpApi.Core
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
