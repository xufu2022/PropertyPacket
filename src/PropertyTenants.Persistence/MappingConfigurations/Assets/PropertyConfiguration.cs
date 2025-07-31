using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Assets;
using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Persistence.MappingConfigurations.Assets
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");
            builder.HasKey(p => p.Id);
            builder.HasOne(a => a.Host)
                .WithMany()
                .HasForeignKey(p => p.HostId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasMany(a => a.Bookings)
                .WithOne(b => b.Property)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.Property<int>("AddressId")
                .HasColumnName("AddressId")
                .IsRequired();

            builder.HasOne(p => p.Address)
                .WithOne()
                .HasForeignKey<Property>("AddressId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne(p => p.PropertyDetail)
                .WithOne(p=>p.Property)
                .HasForeignKey<PropertyDetail>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade).IsRequired(); 
            builder.Navigation(p => p.PropertyDetail).IsRequired();
            builder.Property(p => p.Timestamp)
                .IsRowVersion();

        }


    }

    public class PropertyDetailConfiguration : IEntityTypeConfiguration<PropertyDetail>
    {
        public void Configure(EntityTypeBuilder<PropertyDetail> builder)
        {
            builder.ToTable("Properties");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Description)
                .HasColumnName("Description")
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(d => d.MaxGuests)
                .HasColumnName("MaxGuests")
                .IsRequired();

            builder.Property(d => d.Bedrooms)
                .HasColumnName("Bedrooms")
                .IsRequired();

            builder.Property(d => d.Bathrooms)
                .HasColumnName("Bathrooms")
                .IsRequired();

            builder.Property(d => d.HasWifi)
                .HasColumnName("HasWifi")
                .IsRequired();

            builder.Property(d => d.HasKitchen)
                .HasColumnName("HasKitchen")
                .IsRequired();

            builder.Property<string[]>("Photos")
                .HasColumnName("Photos")
                .HasColumnType("nvarchar(max)");

            //builder.HasOne(d => d.Property)
            //    .WithOne(p => p.PropertyDetail)
            //    .HasForeignKey<PropertyDetail>("PropertyId")
            //    .IsRequired();
            builder.Property<byte[]>("Timestamp").IsRowVersion().HasColumnName("Timestamp");
        }
    }
}
