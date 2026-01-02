using Microsoft.EntityFrameworkCore;

namespace Hierarchy.Infrastructure.Persistence.Entities
{
    public class HierarchyDbContext : DbContext
    {
        public HierarchyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<HierarchyTableItemEntity> HierarchyTableItemsEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HierarchyTableItemEntity>().ToTable("Hierarchy Table");
            modelBuilder.Entity<HierarchyTableItemEntity>().HasKey(v => v.Id);
            modelBuilder.Entity<HierarchyTableItemEntity>().HasData(
                new HierarchyTableItemEntity { Id = 1, Name = "Layout" },
                new HierarchyTableItemEntity { Id = 2, ParentId = 1, Name = "Navigator" },
                new HierarchyTableItemEntity { Id = 3, ParentId = 1, Name = "TopBar" },
                new HierarchyTableItemEntity { Id = 4, ParentId = 1, Name = "SiteSettings" },
                new HierarchyTableItemEntity { Id = 5, ParentId = 1, Name = "Footer" }
            );
        }
    }
}
