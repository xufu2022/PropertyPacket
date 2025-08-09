using Microsoft.EntityFrameworkCore;
using PropertyTenants.Persistence;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Features;

namespace PropertyTenants.Gateways.GraphQL.DataLoaders;

public class PropertyByIdDataLoader : BatchDataLoader<Guid, Property>
{
    private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

    public PropertyByIdDataLoader(
        IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<Guid, Property>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, 
        CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();
        
        var properties = await context.Properties
            .Where(p => keys.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        return properties;
    }
}

public class BookingsByPropertyIdDataLoader : GroupedDataLoader<Guid, Booking>
{
    private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

    public BookingsByPropertyIdDataLoader(
        IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<Guid, Booking>> LoadGroupedBatchAsync(
        IReadOnlyList<Guid> keys, 
        CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var bookings = await context.Bookings
            .Where(b => keys.Contains(b.PropertyId))
            .ToListAsync(cancellationToken);

        return bookings.ToLookup(b => b.PropertyId);
    }
}

public class UserByIdDataLoader : BatchDataLoader<Guid, User>
{
    private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

    public UserByIdDataLoader(
        IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<IReadOnlyDictionary<Guid, User>> LoadBatchAsync(
        IReadOnlyList<Guid> keys, 
        CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();
        
        var users = await context.Users
            .Where(u => keys.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, cancellationToken);

        return users;
    }
}

public class ReviewsByPropertyIdDataLoader : GroupedDataLoader<Guid, Review>
{
    private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

    public ReviewsByPropertyIdDataLoader(
        IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<Guid, Review>> LoadGroupedBatchAsync(
        IReadOnlyList<Guid> keys, 
        CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var reviews = await context.Reviews
            .Where(r => keys.Contains(r.PropertyId))
            .ToListAsync(cancellationToken);

        return reviews.ToLookup(r => r.PropertyId);
    }
}

public class FeaturesByPropertyIdDataLoader : GroupedDataLoader<Guid, Feature>
{
    private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

    public FeaturesByPropertyIdDataLoader(
        IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory;
    }

    protected override async Task<ILookup<Guid, Feature>> LoadGroupedBatchAsync(
        IReadOnlyList<Guid> keys, 
        CancellationToken cancellationToken)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var features = await context.PropertyFeatures
            .Where(pf => keys.Contains(pf.PropertyId))
            .Include(pf => pf.Feature)
            .Select(pf => new { pf.PropertyId, pf.Feature })
            .ToListAsync(cancellationToken);

        return features.ToLookup(f => f.PropertyId, f => f.Feature);
    }
}
