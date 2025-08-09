namespace PropertyTenants.Gateways.GraphQL.Types.EnumTypes;

public enum BookingStatus
{
    Pending,
    Confirmed,
    CheckedIn,
    CheckedOut,
    Cancelled,
    Completed
}

public class BookingStatusType : EnumType<BookingStatus>
{
    protected override void Configure(IEnumTypeDescriptor<BookingStatus> descriptor)
    {
        descriptor
            .Name("BookingStatus")
            .Description("The status of a booking");

        descriptor
            .Value(BookingStatus.Pending)
            .Description("Booking is pending confirmation");

        descriptor
            .Value(BookingStatus.Confirmed)
            .Description("Booking has been confirmed");

        descriptor
            .Value(BookingStatus.CheckedIn)
            .Description("Guest has checked in");

        descriptor
            .Value(BookingStatus.CheckedOut)
            .Description("Guest has checked out");

        descriptor
            .Value(BookingStatus.Cancelled)
            .Description("Booking has been cancelled");

        descriptor
            .Value(BookingStatus.Completed)
            .Description("Booking has been completed");
    }
}
