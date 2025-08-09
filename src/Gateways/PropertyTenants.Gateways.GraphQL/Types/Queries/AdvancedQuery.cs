using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Authorization;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Persistence;
using PropertyTenants.Gateways.GraphQL.DataLoaders;
using PropertyTenants.Gateways.GraphQL.Types.InputTypes;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types.Queries
{
    public class AdvancedQuery
    {
        // ========== PROPERTY QUERIES ==========
        
        /// <summary>
        /// Get all properties with advanced filtering, sorting, and pagination
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        public IQueryable<Property> GetProperties([ScopedService] PropertyTenantsDbContext context)
            => context.Properties
                .Include(p => p.Host)
                .Include(p => p.Address)
                .Include(p => p.PropertyDetail)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature);

        /// <summary>
        /// Get a specific property by ID with DataLoader optimization
        /// </summary>
        public async Task<Property?> GetProperty(
            Guid id,
            PropertyDataLoader propertyLoader,
            CancellationToken cancellationToken)
            => await propertyLoader.LoadAsync(id, cancellationToken);

        /// <summary>
        /// Get properties by host with advanced grouping
        /// </summary>
        public async Task<IEnumerable<Property>> GetPropertiesByHost(
            Guid hostId,
            PropertiesByHostDataLoader propertiesByHostLoader,
            CancellationToken cancellationToken)
            => await propertiesByHostLoader.LoadAsync(hostId, cancellationToken);

        /// <summary>
        /// Search properties with advanced text search and geo-location
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        public async Task<IQueryable<Property>> SearchProperties(
            [ScopedService] PropertyTenantsDbContext context,
            string? searchTerm = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            PropertyType? propertyType = null,
            PropertyStatus? status = null,
            string? city = null,
            string? country = null,
            DateTime? availableFrom = null,
            DateTime? availableTo = null,
            List<Guid>? requiredFeatures = null)
        {
            var query = context.Properties
                .Include(p => p.Host)
                .Include(p => p.Address)
                .Include(p => p.PropertyDetail)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature)
                .AsQueryable();

            // Advanced text search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => 
                    EF.Functions.Contains(p.Title, searchTerm) ||
                    EF.Functions.Contains(p.PropertyDetail!.Description, searchTerm) ||
                    EF.Functions.Contains(p.Address.City, searchTerm) ||
                    EF.Functions.Contains(p.Address.Country, searchTerm));
            }

            // Price range filtering
            if (minPrice.HasValue)
                query = query.Where(p => p.PricePerNight >= minPrice.Value);
            
            if (maxPrice.HasValue)
                query = query.Where(p => p.PricePerNight <= maxPrice.Value);

            // Property type filtering
            if (propertyType.HasValue)
                query = query.Where(p => p.Type == propertyType.Value);

            // Status filtering
            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);

            // Location filtering
            if (!string.IsNullOrEmpty(city))
                query = query.Where(p => EF.Functions.Like(p.Address.City, $"%{city}%"));
            
            if (!string.IsNullOrEmpty(country))
                query = query.Where(p => EF.Functions.Like(p.Address.Country, $"%{country}%"));

            // Availability filtering
            if (availableFrom.HasValue && availableTo.HasValue)
            {
                query = query.Where(p => !p.Bookings.Any(b => 
                    (b.CheckInDate <= availableTo.Value && b.CheckOutDate >= availableFrom.Value) &&
                    b.Status == "Confirmed"));
            }

            // Feature filtering
            if (requiredFeatures != null && requiredFeatures.Any())
            {
                foreach (var featureId in requiredFeatures)
                {
                    query = query.Where(p => p.PropertyFeatures.Any(pf => pf.FeatureId == featureId));
                }
            }

            return query;
        }

        /// <summary>
        /// Get property analytics and statistics
        /// </summary>
        [Authorize(Policy = "HostOrAdmin")]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<PropertyAnalytics> GetPropertyAnalytics(
            [ScopedService] PropertyTenantsDbContext context,
            Guid propertyId,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            fromDate ??= DateTime.UtcNow.AddMonths(-12);
            toDate ??= DateTime.UtcNow;

            var property = await context.Properties
                .Include(p => p.Bookings)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == propertyId);

            if (property == null)
                throw new GraphQLException("Property not found");

            var bookings = property.Bookings
                .Where(b => b.BookedAt >= fromDate && b.BookedAt <= toDate)
                .ToList();

            var reviews = property.Reviews
                .Where(r => r.CreatedAt >= fromDate && r.CreatedAt <= toDate)
                .ToList();

            return new PropertyAnalytics
            {
                PropertyId = propertyId,
                TotalBookings = bookings.Count,
                TotalRevenue = bookings.Sum(b => b.TotalPrice),
                AverageBookingValue = bookings.Any() ? bookings.Average(b => b.TotalPrice) : 0,
                OccupancyRate = CalculateOccupancyRate(bookings, fromDate.Value, toDate.Value),
                AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0,
                TotalReviews = reviews.Count,
                PeriodStart = fromDate.Value,
                PeriodEnd = toDate.Value
            };
        }

        // ========== USER QUERIES ==========

        /// <summary>
        /// Get all users with advanced filtering and role-based access
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        [Authorize(Policy = "AdminOnly")]
        public IQueryable<User> GetUsers([ScopedService] PropertyTenantsDbContext context)
            => context.Users
                .Include(u => u.ContactInfo)
                .Include(u => u.HostedProperties)
                .Include(u => u.Bookings);

        /// <summary>
        /// Get a specific user by ID with DataLoader optimization
        /// </summary>
        public async Task<User?> GetUser(
            Guid id,
            UserDataLoader userLoader,
            CancellationToken cancellationToken)
            => await userLoader.LoadAsync(id, cancellationToken);

        /// <summary>
        /// Get current user information
        /// </summary>
        [Authorize]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<User?> GetMe(
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            return await context.Users
                .Include(u => u.ContactInfo)
                .Include(u => u.HostedProperties)
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);
        }

        /// <summary>
        /// Search users with advanced criteria
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        [Authorize(Policy = "AdminOnly")]
        public IQueryable<User> SearchUsers(
            [ScopedService] PropertyTenantsDbContext context,
            string? searchTerm = null,
            bool? isHost = null,
            bool? isGuest = null,
            string? city = null,
            string? country = null)
        {
            var query = context.Users
                .Include(u => u.ContactInfo)
                .Include(u => u.HostedProperties)
                .Include(u => u.Bookings)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u =>
                    EF.Functions.Contains(u.FriendlyName, searchTerm) ||
                    EF.Functions.Contains(u.ClientName, searchTerm) ||
                    EF.Functions.Contains(u.UserName, searchTerm) ||
                    EF.Functions.Contains(u.ContactInfo.Email, searchTerm));
            }

            if (isHost.HasValue)
                query = query.Where(u => u.IsHost == isHost.Value);

            if (isGuest.HasValue)
                query = query.Where(u => u.IsGuest == isGuest.Value);

            return query;
        }

        // ========== BOOKING QUERIES ==========

        /// <summary>
        /// Get all bookings with advanced filtering
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        [Authorize]
        public IQueryable<Booking> GetBookings([ScopedService] PropertyTenantsDbContext context)
            => context.Bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Address)
                .Include(b => b.Guest)
                    .ThenInclude(g => g.ContactInfo);

        /// <summary>
        /// Get a specific booking by ID
        /// </summary>
        public async Task<Booking?> GetBooking(
            Guid id,
            BookingDataLoader bookingLoader,
            CancellationToken cancellationToken)
            => await bookingLoader.LoadAsync(id, cancellationToken);

        /// <summary>
        /// Get bookings by property
        /// </summary>
        public async Task<IEnumerable<Booking>> GetBookingsByProperty(
            Guid propertyId,
            BookingsByPropertyDataLoader bookingsByPropertyLoader,
            CancellationToken cancellationToken)
            => await bookingsByPropertyLoader.LoadAsync(propertyId, cancellationToken);

        /// <summary>
        /// Get user's bookings with advanced filtering
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        [Authorize]
        public async Task<IQueryable<Booking>> GetMyBookings(
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user,
            BookingStatus? status = null,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            var userId = user.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new GraphQLException("User not authenticated");

            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not found");

            var query = context.Bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Address)
                .Include(b => b.Guest)
                .Where(b => b.GuestId == currentUser.Id);

            if (status.HasValue)
                query = query.Where(b => b.Status == status.Value.ToString());

            if (fromDate.HasValue)
                query = query.Where(b => b.CheckInDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(b => b.CheckOutDate <= toDate.Value);

            return query;
        }

        // ========== REVIEW QUERIES ==========

        /// <summary>
        /// Get all reviews with advanced filtering
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        public IQueryable<Review> GetReviews([ScopedService] PropertyTenantsDbContext context)
            => context.Reviews
                .Include(r => r.Property)
                .Include(r => r.Reviewer)
                .Include(r => r.Reviewee);

        /// <summary>
        /// Get reviews by property
        /// </summary>
        public async Task<IEnumerable<Review>> GetReviewsByProperty(
            Guid propertyId,
            ReviewsByPropertyDataLoader reviewsByPropertyLoader,
            CancellationToken cancellationToken)
            => await reviewsByPropertyLoader.LoadAsync(propertyId, cancellationToken);

        /// <summary>
        /// Get review analytics
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<ReviewAnalytics> GetReviewAnalytics(
            [ScopedService] PropertyTenantsDbContext context,
            Guid? propertyId = null,
            Guid? userId = null)
        {
            var query = context.Reviews.AsQueryable();

            if (propertyId.HasValue)
                query = query.Where(r => r.PropertyId == propertyId.Value);

            if (userId.HasValue)
                query = query.Where(r => r.ReviewerId == userId.Value || r.RevieweeId == userId.Value);

            var reviews = await query.ToListAsync();

            return new ReviewAnalytics
            {
                TotalReviews = reviews.Count,
                AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0,
                RatingDistribution = reviews
                    .GroupBy(r => Math.Floor(r.Rating))
                    .ToDictionary(g => (int)g.Key, g => g.Count()),
                LatestReviews = reviews
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(10)
                    .ToList()
            };
        }

        // ========== FEATURE QUERIES ==========

        /// <summary>
        /// Get all features with usage statistics
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [UsePaging]
        public IQueryable<Feature> GetFeatures([ScopedService] PropertyTenantsDbContext context)
            => context.Features.Include(f => f.PropertyFeatures);

        /// <summary>
        /// Get feature by ID
        /// </summary>
        public async Task<Feature?> GetFeature(
            Guid id,
            FeatureDataLoader featureLoader,
            CancellationToken cancellationToken)
            => await featureLoader.LoadAsync(id, cancellationToken);

        // ========== UTILITY METHODS ==========

        private static decimal CalculateOccupancyRate(List<Booking> bookings, DateTime fromDate, DateTime toDate)
        {
            var totalDays = (toDate - fromDate).Days;
            if (totalDays <= 0) return 0;

            var bookedDays = bookings
                .Where(b => b.Status == "Confirmed")
                .Sum(b => (b.CheckOutDate - b.CheckInDate).Days);

            return totalDays > 0 ? (decimal)bookedDays / totalDays * 100 : 0;
        }
    }

    // ========== ANALYTICS TYPES ==========

    public class PropertyAnalytics
    {
        public Guid PropertyId { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageBookingValue { get; set; }
        public decimal OccupancyRate { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }

    public class ReviewAnalytics
    {
        public int TotalReviews { get; set; }
        public decimal AverageRating { get; set; }
        public Dictionary<int, int> RatingDistribution { get; set; } = new();
        public List<Review> LatestReviews { get; set; } = new();
    }

    // ========== ENUMS ==========

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed,
        NoShow
    }
}
