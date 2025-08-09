using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Persistence;
using HotChocolate.Types.Relay;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class ReviewType : ObjectType<Review>
{
    protected override void Configure(IObjectTypeDescriptor<Review> descriptor)
    {
        descriptor
            .Name("Review")
            .Description("A review for a property")
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => 
                ctx.DataLoader<ReviewByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor
            .Field(f => f.Id)
            .ID(nameof(Review))
            .Description("The unique identifier of the review");

        descriptor
            .Field(f => f.Rating)
            .Description("Rating given in the review (1-5)")
            .Type<NonNullType<IntType>>();

        descriptor
            .Field(f => f.Comment)
            .Description("Comment text of the review")
            .Type<StringType>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the review was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.Property)
            .Description("The property being reviewed")
            .ResolveWith<ReviewResolvers>(r => r.GetPropertyAsync(default!, default!, default!))
            .Type<NonNullType<PropertyType>>();

        descriptor
            .Field(f => f.User)
            .Description("The user who wrote the review")
            .ResolveWith<ReviewResolvers>(r => r.GetUserAsync(default!, default!, default!))
            .Type<NonNullType<UserType>>();

        descriptor
            .Field(f => f.Booking)
            .Description("The booking this review is for")
            .ResolveWith<ReviewResolvers>(r => r.GetBookingAsync(default!, default!, default!))
            .Type<BookingType>();
    }
}

public class ReviewResolvers
{
    public async Task<Property> GetPropertyAsync(
        [Parent] Review review,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Properties
            .FirstAsync(p => p.Id == review.PropertyId, cancellationToken);
    }

    public async Task<User> GetUserAsync(
        [Parent] Review review,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstAsync(u => u.Id == review.UserId, cancellationToken);
    }

    public async Task<Booking?> GetBookingAsync(
        [Parent] Review review,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Bookings
            .FirstOrDefaultAsync(b => b.Id == review.BookingId, cancellationToken);
    }
}
