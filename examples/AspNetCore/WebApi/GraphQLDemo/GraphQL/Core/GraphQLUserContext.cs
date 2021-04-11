using System.Collections.Generic;
using System.Security.Claims;

namespace GraphQLDemo.GraphQL.Core
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal User { get; set; }
    }
}
