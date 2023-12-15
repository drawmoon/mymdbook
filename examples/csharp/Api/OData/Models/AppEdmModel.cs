using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace OData.Models
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
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Read")))
                .HasReadByKeyRestrictions(r =>
                    r.HasPermissions(p => p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.ReadByKey"))));

            users
                .HasInsertRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Create")));

            users
                .HasUpdateRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Update")));

            users.HasDeleteRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Schema").HasScopes(s => s.HasScope("User.Delete")));

            builder.EntitySet<Order>("Orders");

            builder.EntitySet<OrderDetail>("OrderDetails");

            return builder.GetEdmModel();
        }
    }
}
