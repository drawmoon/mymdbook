using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataDemo.Models
{
    public static class AppEdmModel
    {
        public static IEdmModel GetModel()
        {
            ODataConventionModelBuilder builder = new();
            var users = builder.EntitySet<User>("Users");

            users
                .HasReadRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasRestrictedProperties("User.Read")))
                .HasReadByKeyRestrictions(r =>
                    r.HasPermissions(p => p.HasSchemeName("Schema").HasScopes(s => s.HasRestrictedProperties("User.ReadByKey"))));

            return builder.GetEdmModel();
        }
    }
}
