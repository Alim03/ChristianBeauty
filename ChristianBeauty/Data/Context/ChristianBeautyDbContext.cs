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
        public DbSet<Blog>  Blogs{ get; set; }
        public DbSet<LoyaltyClubUser> LoyaltyClubUser{ get; set; }

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

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            foreach (var entry in ChangeTracker.Entries<Product>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    // case EntityState.Modified:
                    //     entry.Entity.DateModified = DateTime.UtcNow;
                    //     break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
