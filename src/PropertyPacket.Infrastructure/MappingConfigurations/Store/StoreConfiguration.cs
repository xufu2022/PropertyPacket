using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyPacket.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyPacket.Infrastructure.MappingConfigurations.Store
{
    internal class StoreConfiguration : IEntityTypeConfiguration<PropertyPacket.Domain.Store.Store>
    {
        public void Configure(EntityTypeBuilder<Domain.Store.Store> builder)
        {
            builder.ToTable("Stores");
            builder.HasOne(e=>e.StoreInfo).WithOne()
                .HasForeignKey<Domain.Store.StoreInfo>(e => e.StoreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
