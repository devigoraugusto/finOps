using Microsoft.EntityFrameworkCore;
using finOps.Core.Entities;

namespace finOps.Infra.Persistence.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<Invoice>().ToTable("Invoices");

            base.OnModelCreating(modelBuilder);
        }
    }
}