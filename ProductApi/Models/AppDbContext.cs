using System.Collections.Generic;
using System.Reflection.Emit;
using ProductModel;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryCode)
                .HasPrincipalKey(c => c.Code)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
