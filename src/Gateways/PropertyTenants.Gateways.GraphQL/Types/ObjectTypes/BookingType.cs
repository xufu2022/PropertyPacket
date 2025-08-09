using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Persistence;
using HotChocolate.Types.Relay;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class BookingType : ObjectType<Booking>
{
    protected override void Configure(IObjectTypeDescriptor<Booking> descriptor)
    {
        descriptor
            .Name("Booking")
            .Description("A booking for a property")
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => 
                ctx.DataLoader<BookingByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor
            .Field(f => f.Id)
            .ID(nameof(Booking))
            .Description("The unique identifier of the booking");

        descriptor
            .Field(f => f.CheckInDate)
            .Description("Check-in date")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.CheckOutDate)
            .Description("Check-out date")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.NumberOfGuests)
            .Description("Number of guests")
            .Type<NonNullType<IntType>>();

        descriptor
            .Field(f => f.TotalPrice)
            .Description("Total price of the booking")
            .Type<NonNullType<DecimalType>>();

        descriptor
            .Field(f => f.BookedAt)
            .Description("When the booking was made")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.Status)
            .Description("Current status of the booking")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Property)
            .Description("The property being booked")
            .ResolveWith<BookingResolvers>(r => r.GetPropertyAsync(default!, default!, default!))
            .Type<NonNullType<PropertyType>>();

        descriptor
            .Field(f => f.Guest)
            .Description("The guest making the booking")
            .ResolveWith<BookingResolvers>(r => r.GetGuestAsync(default!, default!, default!))
            .Type<NonNullType<UserType>>();

        descriptor
            .Field(f => f.Review)
            .Description("Review for this booking")
            .ResolveWith<BookingResolvers>(r => r.GetReviewAsync(default!, default!, default!))
            .Type<ReviewType>();

        // Computed fields
        descriptor
            .Field("duration")
            .Description("Duration of the booking in days")
            .Type<NonNullType<IntType>>()
            .Resolve(context =>
            {
                var booking = context.Parent<Booking>();
                return (booking.CheckOutDate - booking.CheckInDate).Days;
            });

        descriptor
            .Field("pricePerNight")
            .Description("Price per night calculated from total price and duration")
            .Type<NonNullType<DecimalType>>()
            .Resolve(context =>
            {
                var booking = context.Parent<Booking>();
                var duration = (booking.CheckOutDate - booking.CheckInDate).Days;
                return duration > 0 ? booking.TotalPrice / duration : 0;
            });
    }
}

public class BookingResolvers
{
    public async Task<Property> GetPropertyAsync(
        [Parent] Booking booking,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Properties
            .FirstAsync(p => p.Id == booking.PropertyId, cancellationToken);
    }

    public async Task<User> GetGuestAsync(
        [Parent] Booking booking,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstAsync(u => u.Id == booking.GuestId, cancellationToken);
    }

    public async Task<Review?> GetReviewAsync(
        [Parent] Booking booking,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Reviews
            .FirstOrDefaultAsync(r => r.BookingId == booking.Id, cancellationToken);
    }
}
