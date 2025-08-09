using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Gateways.GraphQL.Types.Subscriptions;

[SubscriptionType]
public class Subscription
{
    [Subscribe]
    [Topic("PropertyCreated")]
    public Property PropertyCreated([EventMessage] Property property) => property;

    [Subscribe]
    [Topic("BookingCreated")]
    public Booking BookingCreated([EventMessage] Booking booking) => booking;

    [Subscribe]
    [Topic("UserCreated")]
    public User UserCreated([EventMessage] User user) => user;

    [Subscribe]
    [Topic("PropertyDeleted")]
    public object PropertyDeleted([EventMessage] object deletedInfo) => deletedInfo;
}
