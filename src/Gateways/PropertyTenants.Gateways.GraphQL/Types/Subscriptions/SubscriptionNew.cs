using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Users;

namespace PropertyTenants.Gateways.GraphQL.Types.Subscriptions;

[SubscriptionType]
public class Subscription
{
    [Subscribe]
    public Property PropertyCreated([EventMessage] Property property) => property;

    [Subscribe]
    public Booking BookingCreated([EventMessage] Booking booking) => booking;

    [Subscribe]
    public User UserCreated([EventMessage] User user) => user;
}
