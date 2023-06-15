using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristianBeauty.Data.Context
{
    public class ChristianBeautyDbContext : DbContext
    {
        public ChristianBeautyDbContext(DbContextOptions<ChristianBeautyDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
