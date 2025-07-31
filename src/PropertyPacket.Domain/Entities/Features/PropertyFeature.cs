using PropertyTenants.Domain.Entities.Assets;
using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Domain.Entities.Features
{
    public class PropertyFeature : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Property? Property { get; set; } = null!;
        public int FeatureId { get; set; }
        public Feature? Feature { get; set; } = null!;
    }
}