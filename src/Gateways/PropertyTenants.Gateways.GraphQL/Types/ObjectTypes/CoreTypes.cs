using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class PropertyType : ObjectType<Property>
{
    protected override void Configure(IObjectTypeDescriptor<Property> descriptor)
    {
        descriptor
            .Name("Property")
            .Description("A rental property in the system");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Title)
            .Description("The property title")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.PricePerNight)
            .Description("Price per night in USD")
            .Type<NonNullType<DecimalType>>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the property was created")
            .Type<NonNullType<DateTimeType>>();

        // Computed field for formatted price
        descriptor
            .Field("formattedPrice")
            .Description("Formatted price with currency")
            .Type<NonNullType<StringType>>()
            .Resolve(context => 
            {
                var property = context.Parent<Property>();
                return $"${property.PricePerNight:F2} USD";
            });
    }
}

[ObjectType]
public class BookingType : ObjectType<Booking>
{
    protected override void Configure(IObjectTypeDescriptor<Booking> descriptor)
    {
        descriptor
            .Name("Booking")
            .Description("A property booking");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.CheckInDate)
            .Description("Check-in date")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.CheckOutDate)
            .Description("Check-out date")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.TotalPrice)
            .Description("Total booking price")
            .Type<NonNullType<DecimalType>>();

        descriptor
            .Field(f => f.Status)
            .Description("Booking status")
            .Type<NonNullType<StringType>>();

        // Computed field for booking duration
        descriptor
            .Field("duration")
            .Description("Booking duration in days")
            .Type<NonNullType<IntType>>()
            .Resolve(context => 
            {
                var booking = context.Parent<Booking>();
                return (booking.CheckOutDate - booking.CheckInDate).Days;
            });
    }
}

[ObjectType]
public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .Name("User")
            .Description("A user in the system");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.FriendlyName)
            .Description("The user's friendly name")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.ClientName)
            .Description("The client name")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.UserName)
            .Description("The username")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Email)
            .Description("The user's email address")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.IsHost)
            .Description("Whether the user is a host")
            .Type<NonNullType<BooleanType>>();

        descriptor
            .Field(f => f.IsGuest)
            .Description("Whether the user is a guest")
            .Type<NonNullType<BooleanType>>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the user was created")
            .Type<NonNullType<DateTimeType>>();

        // Hide sensitive fields
        descriptor
            .Field(f => f.PasswordHash)
            .Ignore();

        // Computed field for display name
        descriptor
            .Field("displayName")
            .Description("The user's display name")
            .Type<NonNullType<StringType>>()
            .Resolve(context => 
            {
                var user = context.Parent<User>();
                return user.FriendlyName ?? user.UserName;
            });
    }
}

[ObjectType]
public class ReviewType : ObjectType<Review>
{
    protected override void Configure(IObjectTypeDescriptor<Review> descriptor)
    {
        descriptor
            .Name("Review")
            .Description("A property review");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Rating)
            .Description("Rating from 1 to 5")
            .Type<NonNullType<DecimalType>>();

        descriptor
            .Field(f => f.Comment)
            .Description("Review comment")
            .Type<StringType>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the review was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.PropertyId)
            .Description("The ID of the reviewed property")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.ReviewerId)
            .Description("The ID of the reviewer")
            .Type<NonNullType<UuidType>>();
    }
}
