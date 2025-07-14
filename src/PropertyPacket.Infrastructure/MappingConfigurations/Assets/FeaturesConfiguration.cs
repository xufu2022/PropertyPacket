using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Assets;

namespace PropertyTenants.Infrastructure.MappingConfigurations.Assets;

public class FeatureGroupConfiguration : IEntityTypeConfiguration<FeatureGroup>
{
    public void Configure(EntityTypeBuilder<FeatureGroup> builder)
    {
        builder.ToTable("FeatureGroups");
    }
}

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.ToTable("Features");
        builder.HasOne(f => f.FeatureGroup)
            .WithMany(fg => fg.Features)
            .HasForeignKey(f => f.FeatureGroupId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}

public class PropertyFeatureConfiguration : IEntityTypeConfiguration<PropertyFeature>
{
    public void Configure(EntityTypeBuilder<PropertyFeature> builder)
    {
        builder.ToTable("PropertyFeatures");
        builder.HasAlternateKey(pf => new { pf.PropertyId, pf.FeatureId });

        builder.HasOne(pf => pf.Property)
            .WithMany(p => p.PropertyFeatures)
            .HasForeignKey(pf => pf.PropertyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasOne(pf => pf.Feature)
            .WithMany()
            .HasForeignKey(pf => pf.FeatureId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}