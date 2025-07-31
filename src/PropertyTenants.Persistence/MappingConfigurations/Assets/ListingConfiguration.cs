using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Assets;

namespace PropertyTenants.Persistence.MappingConfigurations.Assets
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            #region DiscriminatorConfiguration TPH
            builder.Property<DateTime>("_createdAt")
                .HasField("_createdAt")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasDefaultValueSql("GETDATE()");
            //builder.ToTable("Listings");
            //builder
            //    .HasDiscriminator<string>("ListingType")
            //    .HasValue<Apartment>("Apartment")
            //    .HasValue<House>("House")
            //    .HasValue<GuestHome>("GuestHome")
            //    .HasValue<UniqueSpace>("UniqueSpace")
            //    .HasValue<Bed>("Bed")
            //    .HasValue<Hotel>("Hotel");

            #endregion

            #region TPT
            //builder.ToTable("Listings");
            //builder.HasKey(l => l.Id);
            // Configure TPT for derived classes
            //builder.Metadata.SetIsTableExcludedFromMigrations(false); // false will Ensure base table is created; ture will not create base table
            //builder.HasDiscriminator<string>("ListingType").IsComplete(false); // Optional discriminator for clarity
            #endregion

            #region TPC
            builder.UseTpcMappingStrategy();
            #endregion
        }
    }

    //Derived class configurations(unchanged)
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.ToTable("Apartments");
            //builder.HasKey(a => a.Id);
            builder.Property(a => a.FloorNumber);
            builder.Property(a => a.HasElevator);
        }
    }

    public class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.ToTable("Houses");
            //builder.HasKey(h => h.Id);
            builder.Property(h => h.HasBackyard);
            builder.Property(h => h.NumberOfBedrooms);
        }
    }

    public class GuestHomeConfiguration : IEntityTypeConfiguration<GuestHome>
    {
        public void Configure(EntityTypeBuilder<GuestHome> builder)
        {
            builder.ToTable("GuestHomes");
            //builder.HasKey(g => g.Id);
            builder.Property(g => g.IsPrivate);
            builder.Property(g => g.SquareFootage);
        }
    }

    public class UniqueSpaceConfiguration : IEntityTypeConfiguration<UniqueSpace>
    {
        public void Configure(EntityTypeBuilder<UniqueSpace> builder)
        {
            builder.ToTable("UniqueSpaces");
            //builder.HasKey(u => u.Id);
            builder.Property(u => u.UniqueFeature)
                .IsRequired();
            builder.Property(u => u.IsPetFriendly);
        }
    }

    public class BedConfiguration : IEntityTypeConfiguration<Bed>
    {
        public void Configure(EntityTypeBuilder<Bed> builder)
        {
            builder.ToTable("Beds");
            //builder.HasKey(b => b.Id);
            builder.Property(b => b.IncludesBreakfast);
            builder.Property(b => b.RoomNumber);
        }
    }

    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");
            //builder.HasKey(h => h.Id);
            builder.Property(h => h.HasPool);
            builder.Property(h => h.StarRating);
        }
    }
}
