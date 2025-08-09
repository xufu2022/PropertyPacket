using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Subscriptions;
using HotChocolate.Authorization;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types.Subscriptions
{
    public class AdvancedSubscription
    {
        /// <summary>
        /// Subscribe to property creation events
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize(Policy = "HostOrAdmin")]
        public PropertyCreatedEvent OnPropertyCreated([EventMessage] PropertyCreatedEvent propertyCreated)
            => propertyCreated;

        /// <summary>
        /// Subscribe to property updates
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public PropertyUpdatedEvent OnPropertyUpdated([EventMessage] PropertyUpdatedEvent propertyUpdated)
            => propertyUpdated;

        /// <summary>
        /// Subscribe to property status changes (Available, Booked, Maintenance, etc.)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public PropertyStatusChangedEvent OnPropertyStatusChanged([EventMessage] PropertyStatusChangedEvent statusChanged)
            => statusChanged;

        /// <summary>
        /// Subscribe to new user registrations (Admin only)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize(Roles = "Admin")]
        public UserCreatedEvent OnUserCreated([EventMessage] UserCreatedEvent userCreated)
            => userCreated;

        /// <summary>
        /// Subscribe to user profile updates
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public UserUpdatedEvent OnUserUpdated([EventMessage] UserUpdatedEvent userUpdated)
            => userUpdated;

        /// <summary>
        /// Subscribe to user activity events (login, logout, etc.)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize(Policy = "HostOrAdmin")]
        public UserActivityEvent OnUserActivity([EventMessage] UserActivityEvent userActivity)
            => userActivity;

        /// <summary>
        /// Subscribe to new booking creation events
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public async Task<BookingCreatedEvent> OnBookingCreated(
            [EventMessage] BookingCreatedEvent bookingCreated,
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            // Only send to users who are authorized to see this booking
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            var booking = bookingCreated.Booking;
            
            // Allow if user is the guest, host, or admin
            if (booking.GuestId == currentUser.Id || 
                booking.Property.HostId == currentUser.Id || 
                user.IsInRole("Admin"))
            {
                return bookingCreated;
            }

            throw new GraphQLException("Unauthorized to view this booking");
        }

        /// <summary>
        /// Subscribe to booking status changes (Pending, Confirmed, Cancelled, Completed)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public async Task<BookingStatusChangedEvent> OnBookingStatusChanged(
            [EventMessage] BookingStatusChangedEvent statusChanged,
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            // Authorization check similar to OnBookingCreated
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            var booking = statusChanged.Booking;
            
            if (booking.GuestId == currentUser.Id || 
                booking.Property.HostId == currentUser.Id || 
                user.IsInRole("Admin"))
            {
                return statusChanged;
            }

            throw new GraphQLException("Unauthorized to view this booking");
        }

        /// <summary>
        /// Subscribe to booking payment events
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public async Task<BookingPaymentEvent> OnBookingPayment(
            [EventMessage] BookingPaymentEvent paymentEvent,
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            var booking = paymentEvent.Booking;
            
            if (booking.GuestId == currentUser.Id || 
                booking.Property.HostId == currentUser.Id || 
                user.IsInRole("Admin"))
            {
                return paymentEvent;
            }

            throw new GraphQLException("Unauthorized to view this booking payment");
        }

        /// <summary>
        /// Subscribe to new review creation events
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public async Task<ReviewCreatedEvent> OnReviewCreated(
            [EventMessage] ReviewCreatedEvent reviewCreated,
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            var review = reviewCreated.Review;
            
            // Allow if user is the reviewer, reviewee, property owner, or admin
            if (review.ReviewerId == currentUser.Id || 
                review.RevieweeId == currentUser.Id ||
                review.Property.HostId == currentUser.Id ||
                user.IsInRole("Admin"))
            {
                return reviewCreated;
            }

            throw new GraphQLException("Unauthorized to view this review");
        }

        /// <summary>
        /// Subscribe to review updates (responses, disputes, etc.)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public async Task<ReviewUpdatedEvent> OnReviewUpdated(
            [EventMessage] ReviewUpdatedEvent reviewUpdated,
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            var review = reviewUpdated.Review;
            
            if (review.ReviewerId == currentUser.Id || 
                review.RevieweeId == currentUser.Id ||
                review.Property.HostId == currentUser.Id ||
                user.IsInRole("Admin"))
            {
                return reviewUpdated;
            }

            throw new GraphQLException("Unauthorized to view this review");
        }

        /// <summary>
        /// Subscribe to property price changes
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public PropertyPriceChangedEvent OnPropertyPriceChanged([EventMessage] PropertyPriceChangedEvent priceChanged)
            => priceChanged;

        /// <summary>
        /// Subscribe to property availability changes
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public PropertyAvailabilityChangedEvent OnPropertyAvailabilityChanged([EventMessage] PropertyAvailabilityChangedEvent availabilityChanged)
            => availabilityChanged;

        /// <summary>
        /// Subscribe to system notifications for users
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public async Task<SystemNotificationEvent> OnSystemNotification(
            [EventMessage] SystemNotificationEvent notification,
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            // Check if notification is for this user or is a global notification
            if (notification.UserId == null || notification.UserId == currentUser.Id)
            {
                return notification;
            }

            throw new GraphQLException("Notification not for this user");
        }

        /// <summary>
        /// Subscribe to chat messages (for property inquiries)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize]
        public async Task<ChatMessageEvent> OnChatMessage(
            [EventMessage] ChatMessageEvent chatMessage,
            [ScopedService] PropertyTenantsDbContext context,
            ClaimsPrincipal user)
        {
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            // Only allow participants in the chat to receive messages
            if (chatMessage.SenderId == currentUser.Id || 
                chatMessage.RecipientId == currentUser.Id ||
                user.IsInRole("Admin"))
            {
                return chatMessage;
            }

            throw new GraphQLException("Unauthorized to view this chat");
        }

        /// <summary>
        /// Subscribe to property analytics updates (Admin and Host only)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize(Policy = "HostOrAdmin")]
        public PropertyAnalyticsEvent OnPropertyAnalytics([EventMessage] PropertyAnalyticsEvent analytics)
            => analytics;

        /// <summary>
        /// Subscribe to platform-wide analytics (Admin only)
        /// </summary>
        [Subscribe]
        [Topic]
        [Authorize(Roles = "Admin")]
        public PlatformAnalyticsEvent OnPlatformAnalytics([EventMessage] PlatformAnalyticsEvent analytics)
            => analytics;
    }

    // ========== EVENT TYPES ==========

    public class PropertyCreatedEvent
    {
        public Property Property { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class PropertyUpdatedEvent
    {
        public Property Property { get; set; } = null!;
        public string[] UpdatedFields { get; set; } = Array.Empty<string>();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class PropertyStatusChangedEvent
    {
        public Property Property { get; set; } = null!;
        public PropertyStatus OldStatus { get; set; }
        public PropertyStatus NewStatus { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class PropertyPriceChangedEvent
    {
        public Property Property { get; set; } = null!;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class PropertyAvailabilityChangedEvent
    {
        public Property Property { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class UserCreatedEvent
    {
        public User User { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class UserUpdatedEvent
    {
        public User User { get; set; } = null!;
        public string[] UpdatedFields { get; set; } = Array.Empty<string>();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class UserActivityEvent
    {
        public User User { get; set; } = null!;
        public string ActivityType { get; set; } = string.Empty; // Login, Logout, PropertyView, BookingAction, etc.
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class BookingCreatedEvent
    {
        public Booking Booking { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class BookingStatusChangedEvent
    {
        public Booking Booking { get; set; } = null!;
        public string OldStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class BookingPaymentEvent
    {
        public Booking Booking { get; set; } = null!;
        public string PaymentStatus { get; set; } = string.Empty; // Pending, Completed, Failed, Refunded
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class ReviewCreatedEvent
    {
        public Review Review { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class ReviewUpdatedEvent
    {
        public Review Review { get; set; } = null!;
        public string UpdateType { get; set; } = string.Empty; // Response, Dispute, Moderation
        public string[] UpdatedFields { get; set; } = Array.Empty<string>();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class SystemNotificationEvent
    {
        public Guid? UserId { get; set; } // null for global notifications
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Info, Warning, Error, Success
        public string? ActionUrl { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
    }

    public class ChatMessageEvent
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid? PropertyId { get; set; } // If chat is about a specific property
        public string Message { get; set; } = string.Empty;
        public string MessageType { get; set; } = "Text"; // Text, Image, Document, etc.
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
    }

    public class PropertyAnalyticsEvent
    {
        public Guid PropertyId { get; set; }
        public string MetricType { get; set; } = string.Empty; // Views, Bookings, Revenue, Rating
        public object MetricValue { get; set; } = null!;
        public DateTime Period { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    public class PlatformAnalyticsEvent
    {
        public string MetricType { get; set; } = string.Empty; // TotalUsers, TotalProperties, TotalBookings, Revenue
        public object MetricValue { get; set; } = null!;
        public DateTime Period { get; set; }
        public string[] Filters { get; set; } = Array.Empty<string>();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
