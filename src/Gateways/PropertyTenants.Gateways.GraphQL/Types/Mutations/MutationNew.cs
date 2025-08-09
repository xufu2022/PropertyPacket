using PropertyTenants.Persistence;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types.Mutations;

[MutationType]
public class MutationNew
{
    public async Task<Property> CreatePropertyAsync(
        [Service] PropertyTenantsDbContext context,
        string name,
        string description,
        decimal pricePerNight,
        int bedrooms,
        int bathrooms)
    {
        var property = new Property
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            PricePerNight = pricePerNight,
            Bedrooms = bedrooms,
            Bathrooms = bathrooms,
            CreatedAt = DateTime.UtcNow
        };

        context.Properties.Add(property);
        await context.SaveChangesAsync();

        return property;
    }

    public async Task<Booking> CreateBookingAsync(
        [Service] PropertyTenantsDbContext context,
        Guid propertyId,
        Guid guestId,
        DateTime checkInDate,
        DateTime checkOutDate)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyId,
            GuestId = guestId,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            Status = Domain.Enums.BookingStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        context.Bookings.Add(booking);
        await context.SaveChangesAsync();

        return booking;
    }

    public async Task<User> CreateUserAsync(
        [Service] PropertyTenantsDbContext context,
        string firstName,
        string lastName,
        string email)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeletePropertyAsync(
        [Service] PropertyTenantsDbContext context,
        Guid id)
    {
        var property = await context.Properties.FindAsync(id);
        if (property == null) return false;

        context.Properties.Remove(property);
        await context.SaveChangesAsync();

        return true;
    }
}
