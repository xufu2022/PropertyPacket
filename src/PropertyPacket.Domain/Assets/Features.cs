using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Assets
{
    public class FeatureGroup : BaseEntity
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public ICollection<Feature> Features { get; set; } = new List<Feature>();
    }

    public class Feature : BaseEntity
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public int FeatureGroupId { get; set; }

        public FeatureGroup FeatureGroup { get; set; } = null!;

        public ICollection<PropertyFeature> PropertyFeatures { get; set; } = [];
    }

    public class PropertyFeature : BaseEntity
    {
        public Guid PropertyId { get; set; }
        public Property? Property { get; set; } = null!;
        public int FeatureId { get; set; }
        public Feature? Feature { get; set; } = null!;
    }
}