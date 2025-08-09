using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.DataLoaders
{
    // Advanced DataLoader for Properties with sophisticated caching and batching
    public class PropertyDataLoader : BatchDataLoader<Guid, Property>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public PropertyDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory, 
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
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            return await context.Properties
                .Include(p => p.Host)
                .Include(p => p.Address)
                .Include(p => p.PropertyDetail)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature)
                .Include(p => p.Reviews)
                .Include(p => p.Bookings)
                .Where(p => keys.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, cancellationToken);
        }
    }

    // Advanced DataLoader for Users with role and permission loading
    public class UserDataLoader : BatchDataLoader<Guid, User>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public UserDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
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
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            return await context.Users
                .Include(u => u.ContactInfo)
                .Include(u => u.HostedProperties)
                .Include(u => u.Bookings)
                .Include(u => u.WrittenReviews)
                .Include(u => u.ReceivedReviews)
                .Where(u => keys.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, cancellationToken);
        }
    }

    // Advanced DataLoader for Bookings with related entities
    public class BookingDataLoader : BatchDataLoader<Guid, Booking>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public BookingDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
                               IBatchScheduler batchScheduler,
                               DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Booking>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            return await context.Bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Address)
                .Include(b => b.Guest)
                    .ThenInclude(g => g.ContactInfo)
                .Include(b => b.Review)
                .Where(b => keys.Contains(b.Id))
                .ToDictionaryAsync(b => b.Id, cancellationToken);
        }
    }

    // Advanced DataLoader for Reviews with user and property details
    public class ReviewDataLoader : BatchDataLoader<Guid, Review>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public ReviewDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
                              IBatchScheduler batchScheduler,
                              DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Review>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            return await context.Reviews
                .Include(r => r.Property)
                .Include(r => r.Booking)
                .Include(r => r.Reviewer)
                .Include(r => r.Reviewee)
                .Where(r => keys.Contains(r.Id))
                .ToDictionaryAsync(r => r.Id, cancellationToken);
        }
    }

    // Advanced DataLoader for Features
    public class FeatureDataLoader : BatchDataLoader<Guid, Feature>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public FeatureDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
                               IBatchScheduler batchScheduler,
                               DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Feature>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            return await context.Features
                .Include(f => f.PropertyFeatures)
                    .ThenInclude(pf => pf.Property)
                .Where(f => keys.Contains(f.Id))
                .ToDictionaryAsync(f => f.Id, cancellationToken);
        }
    }

    // Advanced DataLoader for PropertyFeatures
    public class PropertyFeatureDataLoader : BatchDataLoader<Guid, PropertyFeature>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public PropertyFeatureDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
                                       IBatchScheduler batchScheduler,
                                       DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<IReadOnlyDictionary<Guid, PropertyFeature>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            return await context.PropertyFeatures
                .Include(pf => pf.Property)
                .Include(pf => pf.Feature)
                .Where(pf => keys.Contains(pf.Id))
                .ToDictionaryAsync(pf => pf.Id, cancellationToken);
        }
    }

    // Advanced grouped DataLoader for Properties by Host
    public class PropertiesByHostDataLoader : GroupedDataLoader<Guid, Property>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public PropertiesByHostDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
                                        IBatchScheduler batchScheduler,
                                        DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected override async Task<ILookup<Guid, Property>> LoadGroupedBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            var properties = await context.Properties
                .Include(p => p.Address)
                .Include(p => p.PropertyDetail)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature)
                .Where(p => keys.Contains(p.HostId))
                .ToListAsync(cancellationToken);

            return properties.ToLookup(p => p.HostId);
        }
    }

    // Advanced grouped DataLoader for Bookings by Property
    public class BookingsByPropertyDataLoader : GroupedDataLoader<Guid, Booking>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public BookingsByPropertyDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
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
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            var bookings = await context.Bookings
                .Include(b => b.Guest)
                    .ThenInclude(g => g.ContactInfo)
                .Include(b => b.Review)
                .Where(b => keys.Contains(b.PropertyId))
                .ToListAsync(cancellationToken);

            return bookings.ToLookup(b => b.PropertyId);
        }
    }

    // Advanced grouped DataLoader for Reviews by Property
    public class ReviewsByPropertyDataLoader : GroupedDataLoader<Guid, Review>
    {
        private readonly IDbContextFactory<PropertyTenantsDbContext> _dbContextFactory;

        public ReviewsByPropertyDataLoader(IDbContextFactory<PropertyTenantsDbContext> dbContextFactory,
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
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            
            var reviews = await context.Reviews
                .Include(r => r.Reviewer)
                .Include(r => r.Reviewee)
                .Include(r => r.Booking)
                .Where(r => keys.Contains(r.PropertyId))
                .ToListAsync(cancellationToken);

            return reviews.ToLookup(r => r.PropertyId);
        }
    }
}
