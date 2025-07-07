using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyPacket.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyPacket.Infrastructure.MappingConfigurations.Catalog
{
    public class CategoryTemplateConfiguration : IEntityTypeConfiguration<CategoryTemplate>
    {
        public void Configure(EntityTypeBuilder<CategoryTemplate> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(400);
            builder.Property(e => e.ViewPath)
                .IsRequired()
                .HasMaxLength(400);
            builder.Property(ct => ct.DisplayOrder)
                  .HasField("_displayOrder");
            builder.Ignore(ct => ct.IsActive);
            builder.ToTable("CategoryTemplate");

        }
    }
}
