using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Persistence;
using HotChocolate.Types.Relay;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class PropertyType : ObjectType<Property>
{
    protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
    {
        descriptor
            .Name("Property")
            .Description("A property that can be booked")
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => 
                ctx.DataLoader<PropertyByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor
            .Field(f => f.Id)
            .ID(nameof(Property))
            .Description("The unique identifier of the property");

        descriptor
            .Field(f => f.Title)
            .Description("The title of the property")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Type)
            .Description("The type of the property")
            .Type<NonNullType<PropertyTypeEnum>>();

        descriptor
            .Field(f => f.Status)
            .Description("The current status of the property")
            .Type<NonNullType<PropertyStatusType>>();

        descriptor
            .Field(f => f.PricePerNight)
            .Description("The price per night for the property")
            .Type<NonNullType<DecimalType>>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the property was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.LastUpdatedAt)
            .Description("When the property was last updated")
            .Type<DateTimeType>();

        descriptor
            .Field(f => f.Address)
            .Description("The address of the property")
            .Type<NonNullType<AddressType>>();

        descriptor
            .Field(f => f.Host)
            .Description("The host of the property")
            .ResolveWith<PropertyResolvers>(r => r.GetHostAsync(default!, default!, default!))
            .Type<UserType>();

        descriptor
            .Field(f => f.Bookings)
            .Description("Bookings for this property")
            .ResolveWith<PropertyResolvers>(r => r.GetBookingsAsync(default!, default!, default!))
            .UsePaging<BookingType>()
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(f => f.Reviews)
            .Description("Reviews for this property")
            .ResolveWith<PropertyResolvers>(r => r.GetReviewsAsync(default!, default!, default!))
            .UsePaging<ReviewType>()
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(f => f.PropertyFeatures)
            .Description("Features of this property")
            .ResolveWith<PropertyResolvers>(r => r.GetPropertyFeaturesAsync(default!, default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(f => f.PropertyDetail)
            .Description("Detailed information about the property")
            .ResolveWith<PropertyResolvers>(r => r.GetPropertyDetailAsync(default!, default!, default!))
            .Type<PropertyDetailType>();
    }
}

public class PropertyResolvers
{
    public async Task<User?> GetHostAsync(
        [Parent] Property property,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Id == property.HostId, cancellationToken);
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync(
        [Parent] Property property,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Bookings
            .Where(b => b.PropertyId == property.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Review>> GetReviewsAsync(
        [Parent] Property property,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Reviews
            .Where(r => r.PropertyId == property.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PropertyFeature>> GetPropertyFeaturesAsync(
        [Parent] Property property,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.PropertyFeatures
            .Where(pf => pf.PropertyId == property.Id)
            .Include(pf => pf.Feature)
            .ToListAsync(cancellationToken);
    }

    public async Task<PropertyDetail?> GetPropertyDetailAsync(
        [Parent] Property property,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Set<PropertyDetail>()
            .FirstOrDefaultAsync(pd => pd.PropertyId == property.Id, cancellationToken);
    }
}
