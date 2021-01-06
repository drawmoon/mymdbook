using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace EFCoreSqliteInMemoryDatabase.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext([NotNull] DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
    }
}
