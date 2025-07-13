using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PropertyTenants.Domain.Assets;
using PropertyTenants.Domain.Clients;
using PropertyTenants.Domain.Store;
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
        //public DbSet<User> Users => Set<User>();

        public DbSet<Store> Stores => Set<Store>();
        public DbSet<StoreInfo> StoreInfos => Set<StoreInfo>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
               => optionsBuilder.ConfigureWarnings(b => b.Ignore(CoreEventId.ShadowPropertyCreated));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyTenantsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
