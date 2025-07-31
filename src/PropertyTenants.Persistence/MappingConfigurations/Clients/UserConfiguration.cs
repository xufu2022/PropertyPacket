using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Persistence.MappingConfigurations.Clients
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasOne(u => u.Address)
                .WithOne() // One-to-one relationship
                .HasForeignKey<User>(a=>a.AddressId) // Foreign key is on the Users table
                .IsRequired();

            builder.ComplexProperty(u => u.ContactInfo, contactInfoBuilder =>
            {
                contactInfoBuilder.Property(ci => ci.Email)
                    .IsRequired()
                    .HasMaxLength(256);
                contactInfoBuilder.Property(ci => ci.PhoneNumber)
                    .HasMaxLength(15)
                    .IsRequired(false);
                contactInfoBuilder.Property(ci => ci.Mobile)
                    .HasMaxLength(15)
                    .IsRequired(false);
                contactInfoBuilder.Property<int>("AddressId").IsRequired();

            });

        }
    }
}
