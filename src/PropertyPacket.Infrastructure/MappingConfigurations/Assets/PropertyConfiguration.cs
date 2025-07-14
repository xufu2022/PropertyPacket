using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Assets;
using PropertyTenants.Domain.Common;

namespace PropertyTenants.Infrastructure.MappingConfigurations.Assets
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");
            builder.HasOne(a=>a.Host)
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
                .IsRequired();

            builder.Property(p => p.Timestamp)
                .IsRowVersion();

        }
    }
}
