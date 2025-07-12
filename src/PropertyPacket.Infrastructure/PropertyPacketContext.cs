using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PropertyPacket.Domain.Catalog;

namespace PropertyPacket.Infrastructure
{
    public class PropertyPacketContext(DbContextOptions<PropertyPacketContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<CategoryTemplate> CategoriesTemplate { get; set; } = null!;

        public DbSet<CategoryHierarchy> CategoryHierarchies => Set<CategoryHierarchy>();

        public DbSet<Domain.Store.Store> Stores => Set<Domain.Store.Store>();
        public DbSet<Domain.Store.StoreInfo> StoreInfos => Set<Domain.Store.StoreInfo>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
               => optionsBuilder.ConfigureWarnings(b => b.Throw(CoreEventId.ShadowPropertyCreated));
        //public DbSet<CategoryTemplate> CategoryTemplates => Set<CategoryTemplate>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyPacketContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
