using DDDExample.Domain.User.Model;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<IdentityUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUser>().Property(x => x.Id).ValueGeneratedOnAdd();
    }
}