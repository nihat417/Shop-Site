using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop_Site.Models;

namespace Shop_Site.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Brand>().HasData(
               new Brand { Id = "1", Name = "SUPREME", CreatedDate = DateTime.Now},
               new Brand { Id = "2", Name = "OFF-WHITE", CreatedDate = DateTime.Now },
               new Brand { Id = "3", Name = "STUSSY", CreatedDate = DateTime.Now },
               new Brand { Id = "4", Name = "VETEMENTS", CreatedDate = DateTime.Now }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = "1", Name = "Bloomers", CreatedDate = DateTime.Now },
                new Category { Id = "2", Name = "Blouse", CreatedDate = DateTime.Now },
                new Category { Id = "3", Name = "Bodysuit", CreatedDate = DateTime.Now },
                new Category { Id = "4", Name = "Coat", CreatedDate = DateTime.Now }
                );

			base.OnModelCreating(modelBuilder);
		}

        public DbSet<Products>Products { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
