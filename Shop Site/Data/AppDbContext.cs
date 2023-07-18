using Microsoft.EntityFrameworkCore;
using Shop_Site.Models;

namespace Shop_Site.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .HasOne(c => c.category).WithMany(p => p.Product)
                .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Products>()
                .HasOne(b=>b.brand).WithMany(p => p.Product)
                .HasForeignKey(p=>p.BrandId).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Products>products { get; set; }
        public DbSet<Category>categories { get; set; }
        public DbSet<Brand> brands { get; set; }

    }
}
