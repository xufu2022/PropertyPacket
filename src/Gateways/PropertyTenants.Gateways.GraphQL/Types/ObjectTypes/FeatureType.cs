using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Persistence;
using HotChocolate.Types.Relay;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes
{
    [ObjectType]
    public class FeatureType : ObjectType<Feature>
    {
        protected override void Configure(IObjectTypeDescriptor<Feature> descriptor)
        {
            descriptor
                .Name("Feature")
                .Description("A feature that can be associated with properties")
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => 
                    ctx.DataLoader<FeatureByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(f => f.Id)
                .ID(nameof(Feature))
                .Description("The unique identifier of the feature");

            descriptor
                .Field(f => f.Name)
                .Description("The name of the feature")
                .Type<NonNullType<StringType>>();

            descriptor
                .Field(f => f.Description)
                .Description("The description of the feature")
                .Type<StringType>();

            descriptor
                .Field(f => f.Icon)
                .Description("Icon representation of the feature")
                .Type<StringType>();

            descriptor
                .Field(f => f.IsActive)
                .Description("Whether the feature is active")
                .Type<NonNullType<BooleanType>>();

            descriptor
                .Field(f => f.FeatureGroup)
                .Description("The group this feature belongs to")
                .ResolveWith<FeatureResolvers>(r => r.GetFeatureGroupAsync(default!, default!, default!))
                .Type<FeatureGroupType>();

            descriptor
                .Field("propertyCount")
                .Description("Number of properties with this feature")
                .Type<NonNullType<IntType>>()
                .ResolveWith<FeatureResolvers>(r => r.GetPropertyCountAsync(default!, default!, default!));

            descriptor
                .Field("properties")
                .Description("Properties that have this feature")
                .ResolveWith<FeatureResolvers>(r => r.GetPropertiesAsync(default!, default!, default!))
                .UsePaging<PropertyType>()
                .UseFiltering()
                .UseSorting();
        }
    }

    public class FeatureResolvers
    {
        public async Task<FeatureGroup?> GetFeatureGroupAsync(
            [Parent] Feature feature,
            [Service] PropertyTenantsDbContext context,
            CancellationToken cancellationToken)
        {
            return await context.FeatureGroups
                .FirstOrDefaultAsync(fg => fg.Id == feature.FeatureGroupId, cancellationToken);
        }

        public async Task<int> GetPropertyCountAsync(
            [Parent] Feature feature,
            [Service] PropertyTenantsDbContext context,
            CancellationToken cancellationToken)
        {
            return await context.PropertyFeatures
                .CountAsync(pf => pf.FeatureId == feature.Id, cancellationToken);
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(
            [Parent] Feature feature,
            [Service] PropertyTenantsDbContext context,
            CancellationToken cancellationToken)
        {
            return await context.PropertyFeatures
                .Where(pf => pf.FeatureId == feature.Id)
                .Select(pf => pf.Property)
                .ToListAsync(cancellationToken);
        }
    }
}
