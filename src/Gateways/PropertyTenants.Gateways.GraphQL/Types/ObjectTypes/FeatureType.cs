using PropertyTenants.Domain.Entities.Features;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes
{
    public class FeatureType : ObjectType<Feature>
    {
        protected override void Configure(IObjectTypeDescriptor<Feature> descriptor)
        {
            descriptor.Name("Feature");
            descriptor.Description("Represents a property feature");

            descriptor.Field(f => f.Id)
                .Description("The unique identifier of the feature");

            descriptor.Field(f => f.Name)
                .Description("The name of the feature");

            descriptor.Field(f => f.Description)
                .Description("The description of the feature");

            descriptor.Field(f => f.FeatureGroupId)
                .Description("The ID of the feature group this feature belongs to");

            descriptor.Field(f => f.FeatureGroup)
                .Description("The feature group this feature belongs to");

            descriptor.Field(f => f.PropertyFeatures)
                .Description("The properties that have this feature");
        }
    }
}
