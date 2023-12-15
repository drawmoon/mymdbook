using DDDExample.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.EntityFrameworkCore.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public DbSet<TEntity> Entities => context.Set<TEntity>();

        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
