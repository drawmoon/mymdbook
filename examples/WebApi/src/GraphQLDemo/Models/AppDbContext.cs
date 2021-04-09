using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne();

            modelBuilder.Entity<OrderDetail>()
                .HasOne(o => o.Order)
                .WithMany();
        }
    }
}
