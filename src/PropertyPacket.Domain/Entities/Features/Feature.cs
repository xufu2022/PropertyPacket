namespace PropertyTenants.Domain.Entities.Features;

public class Feature : BaseEntity, IAggregateRoot
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public int FeatureGroupId { get; set; }

    public FeatureGroup FeatureGroup { get; set; } = null!;

    public ICollection<PropertyFeature> PropertyFeatures { get; set; } = [];
}