using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PropertyTenants.Domain.Assets;
using PropertyTenants.Domain.Clients;
using PropertyTenants.Domain.Common;
using PropertyTenants.Domain.Store;
using PropertyTenants.Infrastructure.MappingConfigurations.Store;
using PropertyTenants.Infrastructure.Seeds;
using System.Threading;
using Address = PropertyTenants.Domain.Common.Address;

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
        public DbSet<Role> Roles=> Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<StoreInfo> StoreInfos => Set<StoreInfo>();
        public DbSet<PropertyFeature> PropertyFeatures => Set<PropertyFeature>();
        public DbSet<FeatureGroup> FeatureGroups => Set<FeatureGroup>();
        public DbSet<Feature> Features => Set<Feature>();

        public DbSet<Domain.Common.Address> Addresses => Set<Domain.Common.Address>();
        public DbSet<Dictionary<string, object>> Blogs => Set<Dictionary<string, object>>("Blog");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(b => b.Ignore(CoreEventId.ShadowPropertyCreated));
            optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                if (!context.Set<Role>().Any())
                {
                    await context.Set<Role>().AddRangeAsync(PropertyTenantsDataSeed.SeedRoles(), cancellationToken);
                }
                if (!context.Set<Address>().Any())
                {
                    //await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Addresses ON", cancellationToken);
                    await context.Set<Address>().AddRangeAsync(PropertyTenantsDataSeed.SeedAddresses(), cancellationToken);

                }
                if (!context.Set<User>().Any())
                {
                    await context.Set<User>().AddRangeAsync(PropertyTenantsDataSeed.SeedUsers(), cancellationToken);
                }
                if (!context.Set<UserRole>().Any())
                {
                    await context.Set<UserRole>().AddRangeAsync(PropertyTenantsDataSeed.SeedUserRoles(), cancellationToken);
                }
                if (!context.Set<FeatureGroup>().Any())
                {
                    await FeatureGroups.AddRangeAsync(PropertyTenantsDataSeed.SeedFeatureGroups(), cancellationToken);
                }
                if (!context.Set<Feature>().Any())
                {
                    await Features.AddRangeAsync(PropertyTenantsDataSeed.SeedFeatures(), cancellationToken);
                }
                if (!context.Set<Property>().Any())
                {
                    await Properties.AddRangeAsync(PropertyTenantsDataSeed.SeedProperties(), cancellationToken);
                }
                if (!context.Set<PropertyFeature>().Any())
                {
                    await PropertyFeatures.AddRangeAsync(PropertyTenantsDataSeed.SeedPropertyFeatures(), cancellationToken);
                }

                await context.SaveChangesAsync(cancellationToken);
                //await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Addresses OFF", cancellationToken);
            });
            optionsBuilder.UseSeeding((context, _) =>
            {
                if (!context.Set<Role>().Any())
                    Roles.AddRange(PropertyTenantsDataSeed.SeedRoles());
                if (!context.Set<Address>().Any())
                {
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Addresses ON");
                    Addresses.AddRange(PropertyTenantsDataSeed.SeedAddresses());

                }

                if (!context.Set<User>().Any())
                    Users.AddRange(PropertyTenantsDataSeed.SeedUsers());
                if (!context.Set<UserRole>().Any())
                    UserRoles.AddRange(PropertyTenantsDataSeed.SeedUserRoles());

                if (!context.Set<FeatureGroup>().Any())
                {
                    FeatureGroups.AddRange(PropertyTenantsDataSeed.SeedFeatureGroups());
                }
                if (!context.Set<Feature>().Any())
                {
                    Features.AddRange(PropertyTenantsDataSeed.SeedFeatures());
                }
                if (!context.Set<Property>().Any())
                {
                    Properties.AddRange(PropertyTenantsDataSeed.SeedProperties());
                }
                if (!context.Set<PropertyFeature>().Any())
                {
                    PropertyFeatures.AddRangeAsync(PropertyTenantsDataSeed.SeedPropertyFeatures());
                }
                context.SaveChanges();
                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Addresses OFF");
            });
        }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Blog", builder =>
            //{
            //    new BlogConfiguration().Configure(builder);
            //});

            modelBuilder.SharedTypeEntity<Dictionary<string, object>>(
                "Blog", bb =>
                {
                    bb.Property<int>("BlogId");
                    bb.Property<string>("Url");
                    bb.Property<DateTime>("LastUpdated");
                });

            //modelBuilder.HasSequence<int>("BookingNumber", schema: "shared")
            //    .StartsAt(1000)
            //    .IncrementsBy(5);
            //modelBuilder.Entity<Booking>()
            //    .Property(o => o.Id)
            //    .HasDefaultValueSql("NEXT VALUE FOR BookingNumber");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PropertyTenantsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
