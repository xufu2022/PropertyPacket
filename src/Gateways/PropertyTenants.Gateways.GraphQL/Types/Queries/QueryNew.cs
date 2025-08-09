using HotChocolate.Data;
using HotChocolate.Types;
using PropertyTenants.Persistence;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Users;
using PropertyTenants.Domain.Entities.Reviews;
using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Domain.Entities.Stores;
using PropertyTenants.Domain.Entities.Directory;
using PropertyTenants.Domain.Entities.Roles;
using PropertyTenants.Domain.Entities.Outbox;
using PropertyTenants.Domain.Entities.Localized;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Gateways.GraphQL.DataLoaders;
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
        => context.Properties;

    public async Task<Property?> GetPropertyByIdAsync(
        Guid id, 
        PropertyByIdDataLoader dataLoader, 
        CancellationToken cancellationToken)
        => await dataLoader.LoadAsync(id, cancellationToken);

    // Bookings queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Booking> GetBookings([Service] PropertyTenantsDbContext context)
        => context.Bookings;

    // Users queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<User> GetUsers([Service] PropertyTenantsDbContext context)
        => context.Users;

    // Reviews queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Review> GetReviews([Service] PropertyTenantsDbContext context)
        => context.Reviews;

    // Features queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Feature> GetFeatures([Service] PropertyTenantsDbContext context)
        => context.Features;

    // FeatureGroups queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<FeatureGroup> GetFeatureGroups([Service] PropertyTenantsDbContext context)
        => context.FeatureGroups;

    // Roles queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Role> GetRoles([Service] PropertyTenantsDbContext context)
        => context.Roles;

    // Stores queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Store> GetStores([Service] PropertyTenantsDbContext context)
        => context.Stores;

    // FileEntries queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<FileEntry> GetFileEntries([Service] PropertyTenantsDbContext context)
        => context.FileEntries;

    // OutboxEvents queries
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<OutboxEvent> GetOutboxEvents([Service] PropertyTenantsDbContext context)
        => context.OutboxEvents;

    // Analytics queries
    public async Task<int> GetTotalPropertiesCountAsync([Service] PropertyTenantsDbContext context)
        => await context.Properties.CountAsync();

    public async Task<decimal> GetAveragePropertyPriceAsync([Service] PropertyTenantsDbContext context)
        => await context.Properties.AverageAsync(p => p.PricePerNight);
}
