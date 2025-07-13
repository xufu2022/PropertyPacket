using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Clients;
using PropertyTenants.Domain.Common;

namespace PropertyTenants.Infrastructure.MappingConfigurations.Clients
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.OwnsOne(u => u.ContactInfo, contactInfoBuilder =>
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

                //contactInfoBuilder.HasOne(ci => ci.Address)
                //    .WithOne().HasForeignKey<ContactInfo>("AddressId")
                //    .IsRequired();
                contactInfoBuilder.HasOne<Address>(ci => ci.Address)
                    .WithOne()
                    .HasForeignKey<ContactInfo>("AddressId")
                    .IsRequired();
            });

        }
    }
}
