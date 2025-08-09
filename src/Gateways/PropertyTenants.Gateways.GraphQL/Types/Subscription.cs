using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Bookings;

namespace PropertyTenants.Gateways.GraphQL.Types;

public class Subscription
{
    [Subscribe]
    [Topic]
    public Property PropertyCreated([EventMessage] Property property) => property;

    [Subscribe]
    [Topic]
    public User UserCreated([EventMessage] User user) => user;

    [Subscribe]
    [Topic]
    public Booking BookingCreated([EventMessage] Booking booking) => booking;

    [Subscribe]
    [Topic]
    public Review ReviewCreated([EventMessage] Review review) => review;
}
