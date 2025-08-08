using PropertyTenants.Domain.Entities.Features;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes
{
    public class FeatureGroupType : ObjectType<FeatureGroup>
    {
        protected override void Configure(IObjectTypeDescriptor<FeatureGroup> descriptor)
        {
            descriptor.Name("FeatureGroup");
            descriptor.Description("Represents a group of related features");

            descriptor.Field(fg => fg.Id)
                .Description("The unique identifier of the feature group");

            descriptor.Field(fg => fg.Name)
                .Description("The name of the feature group");

            descriptor.Field(fg => fg.Description)
                .Description("The description of the feature group");

            descriptor.Field(fg => fg.Features)
                .Description("The features in this group");
        }
    }
}
