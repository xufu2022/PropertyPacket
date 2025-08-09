using HotChocolate.Data;
using HotChocolate.Types;
using PropertyTenants.Persistence;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types.Queries;

[QueryType]
public class Query
{
    // Properties queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Property> GetProperties([Service] PropertyTenantsDbContext context)
        => context.Properties.Include(p => p.Address).Include(p => p.PropertyDetail);

    public async Task<Property?> GetPropertyByIdAsync(
        Guid id,
        [Service] PropertyTenantsDbContext context)
        => await context.Properties
            .Include(p => p.Address)
            .Include(p => p.PropertyDetail)
            .Include(p => p.Host)
            .FirstOrDefaultAsync(p => p.Id == id);

    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Property> SearchProperties(
        [Service] PropertyTenantsDbContext context,
        string? searchTerm = null,
        decimal? minPrice = null,
        decimal? maxPrice = null)
    {
        var query = context.Properties.Include(p => p.Address).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(p => p.Title.Contains(searchTerm));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.PricePerNight >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.PricePerNight <= maxPrice.Value);
        }

        return query;
    }

    // Bookings queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Booking> GetBookings([Service] PropertyTenantsDbContext context)
        => context.Bookings.Include(b => b.Property).Include(b => b.Guest);

    public async Task<Booking?> GetBookingByIdAsync(
        Guid id,
        [Service] PropertyTenantsDbContext context)
        => await context.Bookings
            .Include(b => b.Property)
            .Include(b => b.Guest)
            .FirstOrDefaultAsync(b => b.Id == id);

    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Booking> GetBookingsByPropertyIdAsync(
        Guid propertyId,
        [Service] PropertyTenantsDbContext context)
        => context.Bookings
            .Where(b => b.PropertyId == propertyId)
            .Include(b => b.Guest);

    // Users queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<User> GetUsers([Service] PropertyTenantsDbContext context)
        => context.Users.Include(u => u.UserRoles);

    public async Task<User?> GetUserByIdAsync(
        Guid id,
        [Service] PropertyTenantsDbContext context)
        => await context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Id == id);

    // Reviews queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Review> GetReviews([Service] PropertyTenantsDbContext context)
        => context.Reviews;

    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Review> GetReviewsByPropertyIdAsync(
        Guid propertyId,
        [Service] PropertyTenantsDbContext context)
        => context.Reviews.Where(r => r.PropertyId == propertyId);

    // Analytics queries
    public async Task<int> GetTotalPropertiesCountAsync([Service] PropertyTenantsDbContext context)
        => await context.Properties.CountAsync();

    public async Task<int> GetTotalBookingsCountAsync([Service] PropertyTenantsDbContext context)
        => await context.Bookings.CountAsync();

    public async Task<int> GetTotalUsersCountAsync([Service] PropertyTenantsDbContext context)
        => await context.Users.CountAsync();

    public async Task<decimal> GetAveragePropertyPriceAsync([Service] PropertyTenantsDbContext context)
        => await context.Properties.AverageAsync(p => p.PricePerNight);

    public async Task<IEnumerable<object>> GetTopRatedPropertiesAsync(
        [Service] PropertyTenantsDbContext context,
        int limit = 10)
    {
        return await context.Properties
            .Select(p => new 
            { 
                Property = p, 
                AverageRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = p.Reviews.Count()
            })
            .OrderByDescending(x => x.AverageRating)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IEnumerable<object>> GetBookingStatsByMonthAsync(
        [Service] PropertyTenantsDbContext context,
        int year)
    {
        return await context.Bookings
            .Where(b => b.CheckInDate.Year == year)
            .GroupBy(b => b.CheckInDate.Month)
            .Select(g => new { Month = g.Key, Count = g.Count(), TotalRevenue = g.Sum(b => b.TotalPrice) })
            .OrderBy(x => x.Month)
            .ToListAsync();
    }
}
