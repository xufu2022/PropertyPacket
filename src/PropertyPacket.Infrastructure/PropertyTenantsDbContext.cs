using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PropertyTenants.Domain.Assets;
using PropertyTenants.Domain.Clients;
using PropertyTenants.Domain.Store;
using PropertyTenants.Infrastructure.Seeds;
namespace PropertyTenants.Infrastructure
{
    public class PropertyTenantsDbContext(DbContextOptions<PropertyTenantsDbContext> options) : DbContext(options)
    {

        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Apartment> Apartments => Set<Apartment>();
        public DbSet<House> Houses => Set<House>();
        public DbSet<GuestHome> GuestHomes => Set<GuestHome>();
        public DbSet<UniqueSpace> UniqueSpaces => Set<UniqueSpace>();
        public DbSet<Bed> Beds => Set<Bed>();
        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles { get; set; } //=> Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<StoreInfo> StoreInfos => Set<StoreInfo>();
        public DbSet<PropertyFeature> PropertyFeatures => Set<PropertyFeature>();
        public DbSet<FeatureGroup> FeatureGroups => Set<FeatureGroup>();
        public DbSet<Feature> Features => Set<Feature>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(b => b.Ignore(CoreEventId.ShadowPropertyCreated));
            optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                await Roles.AddRangeAsync(PropertyTenantsDataSeed.SeedRolesAndUsers(), cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            });
            optionsBuilder.UseSeeding((context, _) =>
            {
                Roles.AddRange(PropertyTenantsDataSeed.SeedRolesAndUsers());
                context.SaveChanges();
            });
        }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyTenantsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
