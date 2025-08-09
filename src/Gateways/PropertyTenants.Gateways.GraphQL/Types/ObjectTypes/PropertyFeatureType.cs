using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Persistence;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class PropertyFeatureType : ObjectType<PropertyFeature>
{
    protected override void Configure(IObjectTypeDescriptor<PropertyFeature> descriptor)
    {
        descriptor
            .Name("PropertyFeature")
            .Description("Association between a property and a feature");

        descriptor
            .Field(f => f.PropertyId)
            .Description("The property ID")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.FeatureId)
            .Description("The feature ID")
            .Type<NonNullType<IntType>>();

        descriptor
            .Field(f => f.Value)
            .Description("Optional value for this feature on the property")
            .Type<StringType>();

        descriptor
            .Field(f => f.Property)
            .Description("The property")
            .ResolveWith<PropertyFeatureResolvers>(r => r.GetPropertyAsync(default!, default!, default!))
            .Type<NonNullType<PropertyType>>();

        descriptor
            .Field(f => f.Feature)
            .Description("The feature")
            .ResolveWith<PropertyFeatureResolvers>(r => r.GetFeatureAsync(default!, default!, default!))
            .Type<NonNullType<FeatureType>>();
    }
}

public class PropertyFeatureResolvers
{
    public async Task<Property> GetPropertyAsync(
        [Parent] PropertyFeature propertyFeature,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Properties
            .FirstAsync(p => p.Id == propertyFeature.PropertyId, cancellationToken);
    }

    public async Task<Feature> GetFeatureAsync(
        [Parent] PropertyFeature propertyFeature,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Features
            .FirstAsync(f => f.Id == propertyFeature.FeatureId, cancellationToken);
    }
}
