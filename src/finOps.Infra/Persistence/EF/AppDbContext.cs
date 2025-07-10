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
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.Guid);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
                entity.Property(c => c.DocumentNumber).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.Guid);
                entity.Property(i => i.Amount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(i => i.IssueDate).IsRequired();
                entity.Property(i => i.DueDate).IsRequired();
                entity.Property(i => i.Status).IsRequired();
                
                entity.HasOne(i => i.Company)
                      .WithMany(c => c.Invoices)
                      .HasForeignKey(i => i.CompanyGuid)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}