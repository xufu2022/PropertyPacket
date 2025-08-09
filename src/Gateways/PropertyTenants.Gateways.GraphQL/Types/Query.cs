using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Property> GetProperties([ScopedService] PropertyTenantsDbContext context)
    {
        return context.Properties.AsNoTracking();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers([ScopedService] PropertyTenantsDbContext context)
    {
        return context.Users.AsNoTracking();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Booking> GetBookings([ScopedService] PropertyTenantsDbContext context)
    {
        return context.Bookings.AsNoTracking();
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> GetReviews([ScopedService] PropertyTenantsDbContext context)
    {
        return context.Reviews.AsNoTracking();
    }

    public async Task<Property?> GetPropertyById([ScopedService] PropertyTenantsDbContext context, Guid id)
    {
        return await context.Properties.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<User?> GetUserById([ScopedService] PropertyTenantsDbContext context, Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Booking?> GetBookingById([ScopedService] PropertyTenantsDbContext context, Guid id)
    {
        return await context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Review?> GetReviewById([ScopedService] PropertyTenantsDbContext context, Guid id)
    {
        return await context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
    }

    // Analytics queries
    public async Task<int> GetTotalPropertiesCount([ScopedService] PropertyTenantsDbContext context)
    {
        return await context.Properties.CountAsync();
    }

    public async Task<int> GetTotalUsersCount([ScopedService] PropertyTenantsDbContext context)
    {
        return await context.Users.CountAsync();
    }

    public async Task<int> GetTotalBookingsCount([ScopedService] PropertyTenantsDbContext context)
    {
        return await context.Bookings.CountAsync();
    }

    public async Task<int> GetTotalReviewsCount([ScopedService] PropertyTenantsDbContext context)
    {
        return await context.Reviews.CountAsync();
    }
}
