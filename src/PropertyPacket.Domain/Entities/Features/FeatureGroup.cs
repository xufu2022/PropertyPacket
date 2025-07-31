namespace PropertyTenants.Domain.Entities.Features;

public class FeatureGroup : BaseEntity, IAggregateRoot
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public ICollection<Feature> Features { get; set; } = new List<Feature>();
}