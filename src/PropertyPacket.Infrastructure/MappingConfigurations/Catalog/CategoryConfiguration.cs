using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyPacket.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyPacket.Data.MappingConfigurations.Catalog
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>

    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(c => c.Description)
                .IsRequired();

            builder.Property(c => c.PageSizeOptions)
                .HasMaxLength(200);

            builder.Property(c => c.PriceFrom)
                .HasColumnType("decimal(18, 4)");

            builder.Property(c => c.PriceTo)
                .HasColumnType("decimal(18, 4)");

            builder.Property(c => c.CreatedOnUtc)
                .HasPrecision(6);

            builder.Property(c => c.UpdatedOnUtc)
                .HasPrecision(6);

            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CategoryTemplate>()
                .WithMany()
                .HasForeignKey(c => c.CategoryTemplateId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(); 

        }
    }
}
