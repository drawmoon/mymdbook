using InitIdentityDatabase.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InitIdentityDatabase
{
    public class IdentityDbContext : IdentityDbContext<UserIdentity, UserIdentityRole, string, UserIdentityClaim, UserIdentityUserRole, UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserIdentity>().ToTable("users");
            builder.Entity<UserIdentityClaim>().ToTable("claims");
            builder.Entity<UserIdentityUserRole>().ToTable("user_roles");

            builder.Entity<UserIdentityRole>().ToTable("roles");
            builder.Entity<UserIdentityRoleClaim>().ToTable("role_claims");

            builder.Entity<UserIdentityUserLogin>().ToTable("user_logins");
            builder.Entity<UserIdentityUserToken>().ToTable("user_tokens");
        }
    }
}
