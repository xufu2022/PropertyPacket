using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Authorization;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Domain.Entities.Common;
using PropertyTenants.Persistence;
using PropertyTenants.Gateways.GraphQL.Types.InputTypes;
using PropertyTenants.Gateways.GraphQL.Types.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types.Mutations
{
    public class AdvancedMutation
    {
        // ========== PROPERTY MUTATIONS ==========

        /// <summary>
        /// Create a new property with advanced validation and features
        /// </summary>
        [Authorize(Policy = "HostOrAdmin")]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<PropertyPayload> CreateProperty(
            [ScopedService] PropertyTenantsDbContext context,
            ITopicEventSender eventSender,
            CreatePropertyInput input,
            ClaimsPrincipal user,
            CancellationToken cancellationToken)
        {
            // Validate user
            var userId = user.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new GraphQLException("User not authenticated");

            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId, cancellationToken);

            if (currentUser == null)
                throw new GraphQLException("User not found");

            if (!currentUser.IsHost && !user.IsInRole("Admin"))
                throw new GraphQLException("Only hosts can create properties");

            // Validate input
            var validationErrors = await ValidatePropertyInput(context, input, cancellationToken);
            if (validationErrors.Any())
            {
                return new PropertyPayload
                {
                    Errors = validationErrors
                };
            }

            // Create property with all related entities
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            
            try
            {
                // Create Address
                var address = new Address(
                    0, // ID will be set by EF
                    input.Address.Line1,
                    input.Address.Line2 ?? "",
                    input.Address.City,
                    input.Address.Country,
                    input.Address.PostCode
                );

                // Create Property
                var property = new Property(Guid.NewGuid())
                {
                    Title = input.Title,
                    Type = input.Type,
                    Status = PropertyStatus.Available,
                    HostId = currentUser.Id,
                    Address = address,
                    PricePerNight = input.PricePerNight,
                    CreatedAt = DateTime.UtcNow
                };

                context.Properties.Add(property);
                await context.SaveChangesAsync(cancellationToken);

                // Create PropertyDetail if provided
                if (input.PropertyDetail != null)
                {
                    var propertyDetail = new PropertyDetail(Guid.NewGuid())
                    {
                        PropertyId = property.Id,
                        Description = input.PropertyDetail.Description,
                        MaxGuests = input.PropertyDetail.MaxGuests,
                        Bedrooms = input.PropertyDetail.Bedrooms,
                        Bathrooms = input.PropertyDetail.Bathrooms,
                        SquareFeet = input.PropertyDetail.SquareFeet,
                        CheckInTime = input.PropertyDetail.CheckInTime,
                        CheckOutTime = input.PropertyDetail.CheckOutTime,
                        HouseRules = input.PropertyDetail.HouseRules,
                        Amenities = input.PropertyDetail.Amenities
                    };

                    context.PropertyDetails.Add(propertyDetail);
                }

                // Add Features if provided
                if (input.FeatureIds != null && input.FeatureIds.Any())
                {
                    var features = await context.Features
                        .Where(f => input.FeatureIds.Contains(f.Id))
                        .ToListAsync(cancellationToken);

                    foreach (var feature in features)
                    {
                        var propertyFeature = new PropertyFeature(Guid.NewGuid())
                        {
                            PropertyId = property.Id,
                            FeatureId = feature.Id,
                            Property = property,
                            Feature = feature
                        };

                        context.PropertyFeatures.Add(propertyFeature);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                // Send subscription event
                await eventSender.SendAsync(nameof(AdvancedSubscription.OnPropertyCreated), 
                    new PropertyCreatedEvent { Property = property }, cancellationToken);

                // Load complete property for response
                var createdProperty = await context.Properties
                    .Include(p => p.Host)
                    .Include(p => p.Address)
                    .Include(p => p.PropertyDetail)
                    .Include(p => p.PropertyFeatures)
                        .ThenInclude(pf => pf.Feature)
                    .FirstAsync(p => p.Id == property.Id, cancellationToken);

                return new PropertyPayload
                {
                    Property = createdProperty
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new GraphQLException($"Failed to create property: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing property with advanced validation
        /// </summary>
        [Authorize(Policy = "HostOrAdmin")]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<PropertyPayload> UpdateProperty(
            [ScopedService] PropertyTenantsDbContext context,
            ITopicEventSender eventSender,
            UpdatePropertyInput input,
            ClaimsPrincipal user,
            CancellationToken cancellationToken)
        {
            var property = await context.Properties
                .Include(p => p.Host)
                .Include(p => p.Address)
                .Include(p => p.PropertyDetail)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature)
                .FirstOrDefaultAsync(p => p.Id == input.Id, cancellationToken);

            if (property == null)
            {
                return new PropertyPayload
                {
                    Errors = new[] { new UserError("Property not found", "PROPERTY_NOT_FOUND") }
                };
            }

            // Authorization check
            var userId = user.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId, cancellationToken);

            if (currentUser == null || (property.HostId != currentUser.Id && !user.IsInRole("Admin")))
            {
                return new PropertyPayload
                {
                    Errors = new[] { new UserError("Unauthorized to update this property", "UNAUTHORIZED") }
                };
            }

            // Update property fields
            if (!string.IsNullOrEmpty(input.Title))
                property.Title = input.Title;
            
            if (input.Type.HasValue)
                property.Type = input.Type.Value;
            
            if (input.Status.HasValue)
                property.Status = input.Status.Value;
            
            if (input.PricePerNight.HasValue)
                property.PricePerNight = input.PricePerNight.Value;

            property.LastUpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            // Send subscription event
            await eventSender.SendAsync(nameof(AdvancedSubscription.OnPropertyUpdated),
                new PropertyUpdatedEvent { Property = property }, cancellationToken);

            return new PropertyPayload { Property = property };
        }

        // ========== USER MUTATIONS ==========

        /// <summary>
        /// Create a new user with advanced profile setup
        /// </summary>
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<UserPayload> CreateUser(
            [ScopedService] PropertyTenantsDbContext context,
            ITopicEventSender eventSender,
            CreateUserInput input,
            CancellationToken cancellationToken)
        {
            // Check if user already exists
            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Email == input.Email || u.UserName == input.UserName, cancellationToken);

            if (existingUser != null)
            {
                return new UserPayload
                {
                    Errors = new[] { new UserError("User with this email or username already exists", "USER_EXISTS") }
                };
            }

            // Validate input
            var validationErrors = ValidateUserInput(input);
            if (validationErrors.Any())
            {
                return new UserPayload
                {
                    Errors = validationErrors
                };
            }

            // Create ContactInfo
            var contactInfo = new ContactInfo(
                input.Email,
                input.PhoneNumber ?? "",
                input.Mobile ?? ""
            );

            // Create User
            var user = new User(Guid.NewGuid())
            {
                FriendlyName = input.FriendlyName,
                ClientName = input.ClientName,
                ShortDescription = input.ShortDescription ?? "",
                Description = input.Description ?? "",
                UserName = input.UserName,
                PasswordHash = input.PasswordHash,
                ContactInfo = contactInfo,
                Email = input.Email,
                IsHost = input.IsHost,
                IsGuest = input.IsGuest,
                CreatedAt = DateTime.UtcNow,
                AzureAdB2CUserId = input.AzureAdB2CUserId ?? ""
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            // Send subscription event
            await eventSender.SendAsync(nameof(AdvancedSubscription.OnUserCreated),
                new UserCreatedEvent { User = user }, cancellationToken);

            return new UserPayload { User = user };
        }

        /// <summary>
        /// Update user profile with advanced validation
        /// </summary>
        [Authorize]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<UserPayload> UpdateUserProfile(
            [ScopedService] PropertyTenantsDbContext context,
            ITopicEventSender eventSender,
            UpdateUserInput input,
            ClaimsPrincipal userClaims,
            CancellationToken cancellationToken)
        {
            var userId = userClaims.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new GraphQLException("User not authenticated");

            var user = await context.Users
                .Include(u => u.ContactInfo)
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId, cancellationToken);

            if (user == null)
            {
                return new UserPayload
                {
                    Errors = new[] { new UserError("User not found", "USER_NOT_FOUND") }
                };
            }

            // Update user fields
            if (!string.IsNullOrEmpty(input.FriendlyName))
                user.FriendlyName = input.FriendlyName;
            
            if (!string.IsNullOrEmpty(input.ShortDescription))
                user.ShortDescription = input.ShortDescription;
            
            if (!string.IsNullOrEmpty(input.Description))
                user.Description = input.Description;

            // Update contact info if provided
            if (input.ContactInfo != null)
            {
                user.ContactInfo = new ContactInfo(
                    input.ContactInfo.Email ?? user.ContactInfo.Email,
                    input.ContactInfo.PhoneNumber ?? user.ContactInfo.PhoneNumber,
                    input.ContactInfo.Mobile ?? user.ContactInfo.Mobile
                );
            }

            await context.SaveChangesAsync(cancellationToken);

            // Send subscription event
            await eventSender.SendAsync(nameof(AdvancedSubscription.OnUserUpdated),
                new UserUpdatedEvent { User = user }, cancellationToken);

            return new UserPayload { User = user };
        }

        // ========== BOOKING MUTATIONS ==========

        /// <summary>
        /// Create a booking with advanced availability checking and business rules
        /// </summary>
        [Authorize]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<BookingPayload> CreateBooking(
            [ScopedService] PropertyTenantsDbContext context,
            ITopicEventSender eventSender,
            CreateBookingInput input,
            ClaimsPrincipal userClaims,
            CancellationToken cancellationToken)
        {
            // Get current user
            var userId = userClaims.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId, cancellationToken);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            // Validate booking request
            var validationResult = await ValidateBookingRequest(context, input, currentUser.Id, cancellationToken);
            if (validationResult.Errors.Any())
            {
                return new BookingPayload { Errors = validationResult.Errors };
            }

            var property = validationResult.Property!;

            // Calculate total price
            var nights = (input.CheckOutDate - input.CheckInDate).Days;
            var totalPrice = nights * property.PricePerNight;

            // Create booking
            var booking = new Booking(Guid.NewGuid())
            {
                PropertyId = input.PropertyId,
                GuestId = currentUser.Id,
                CheckInDate = input.CheckInDate,
                CheckOutDate = input.CheckOutDate,
                NumberOfGuests = input.NumberOfGuests,
                TotalPrice = totalPrice,
                Status = "Pending", // Will be confirmed after payment
                BookedAt = DateTime.UtcNow,
                Property = property,
                Guest = currentUser,
                Review = null! // Will be created later when guest submits review
            };

            context.Bookings.Add(booking);
            await context.SaveChangesAsync(cancellationToken);

            // Send subscription event
            await eventSender.SendAsync(nameof(AdvancedSubscription.OnBookingCreated),
                new BookingCreatedEvent { Booking = booking }, cancellationToken);

            // Load complete booking for response
            var createdBooking = await context.Bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Address)
                .Include(b => b.Guest)
                    .ThenInclude(g => g.ContactInfo)
                .FirstAsync(b => b.Id == booking.Id, cancellationToken);

            return new BookingPayload { Booking = createdBooking };
        }

        /// <summary>
        /// Confirm a booking (host action)
        /// </summary>
        [Authorize(Policy = "HostOrAdmin")]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<BookingPayload> ConfirmBooking(
            [ScopedService] PropertyTenantsDbContext context,
            ITopicEventSender eventSender,
            Guid bookingId,
            ClaimsPrincipal userClaims,
            CancellationToken cancellationToken)
        {
            var booking = await context.Bookings
                .Include(b => b.Property)
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);

            if (booking == null)
            {
                return new BookingPayload
                {
                    Errors = new[] { new UserError("Booking not found", "BOOKING_NOT_FOUND") }
                };
            }

            // Authorization check
            var userId = userClaims.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId, cancellationToken);

            if (currentUser == null || (booking.Property.HostId != currentUser.Id && !userClaims.IsInRole("Admin")))
            {
                return new BookingPayload
                {
                    Errors = new[] { new UserError("Unauthorized to confirm this booking", "UNAUTHORIZED") }
                };
            }

            booking.Status = "Confirmed";
            await context.SaveChangesAsync(cancellationToken);

            // Send subscription event
            await eventSender.SendAsync(nameof(AdvancedSubscription.OnBookingStatusChanged),
                new BookingStatusChangedEvent { Booking = booking, NewStatus = "Confirmed" }, cancellationToken);

            return new BookingPayload { Booking = booking };
        }

        // ========== REVIEW MUTATIONS ==========

        /// <summary>
        /// Create a review with advanced validation
        /// </summary>
        [Authorize]
        [UseDbContext(typeof(PropertyTenantsDbContext))]
        public async Task<ReviewPayload> CreateReview(
            [ScopedService] PropertyTenantsDbContext context,
            ITopicEventSender eventSender,
            CreateReviewInput input,
            ClaimsPrincipal userClaims,
            CancellationToken cancellationToken)
        {
            // Get current user
            var userId = userClaims.FindFirst("sub")?.Value;
            var currentUser = await context.Users
                .FirstOrDefaultAsync(u => u.AzureAdB2CUserId == userId, cancellationToken);

            if (currentUser == null)
                throw new GraphQLException("User not authenticated");

            // Validate review request
            var validationResult = await ValidateReviewRequest(context, input, currentUser.Id, cancellationToken);
            if (validationResult.Errors.Any())
            {
                return new ReviewPayload { Errors = validationResult.Errors };
            }

            var booking = validationResult.Booking!;
            var property = validationResult.Property!;

            // Create review
            var review = new Review(Guid.NewGuid())
            {
                PropertyId = input.PropertyId,
                BookingId = input.BookingId,
                ReviewerId = currentUser.Id,
                RevieweeId = property.HostId, // Guest reviews host
                Rating = input.Rating,
                Comment = input.Comment,
                CreatedAt = DateTime.UtcNow,
                Property = property,
                Booking = booking,
                Reviewer = currentUser,
                Reviewee = property.Host!
            };

            context.Reviews.Add(review);
            await context.SaveChangesAsync(cancellationToken);

            // Send subscription event
            await eventSender.SendAsync(nameof(AdvancedSubscription.OnReviewCreated),
                new ReviewCreatedEvent { Review = review }, cancellationToken);

            return new ReviewPayload { Review = review };
        }

        // ========== VALIDATION METHODS ==========

        private async Task<List<UserError>> ValidatePropertyInput(
            PropertyTenantsDbContext context, 
            CreatePropertyInput input, 
            CancellationToken cancellationToken)
        {
            var errors = new List<UserError>();

            if (string.IsNullOrWhiteSpace(input.Title))
                errors.Add(new UserError("Title is required", "TITLE_REQUIRED"));

            if (input.PricePerNight <= 0)
                errors.Add(new UserError("Price per night must be greater than 0", "INVALID_PRICE"));

            if (input.Address == null)
                errors.Add(new UserError("Address is required", "ADDRESS_REQUIRED"));

            // Check for duplicate properties at same address
            if (input.Address != null)
            {
                var existingProperty = await context.Properties
                    .AnyAsync(p => p.Address.Line1 == input.Address.Line1 &&
                                  p.Address.City == input.Address.City &&
                                  p.Address.PostCode == input.Address.PostCode, cancellationToken);

                if (existingProperty)
                    errors.Add(new UserError("A property already exists at this address", "DUPLICATE_ADDRESS"));
            }

            return errors;
        }

        private List<UserError> ValidateUserInput(CreateUserInput input)
        {
            var errors = new List<UserError>();

            if (string.IsNullOrWhiteSpace(input.FriendlyName))
                errors.Add(new UserError("Friendly name is required", "FRIENDLY_NAME_REQUIRED"));

            if (string.IsNullOrWhiteSpace(input.Email))
                errors.Add(new UserError("Email is required", "EMAIL_REQUIRED"));

            if (!IsValidEmail(input.Email))
                errors.Add(new UserError("Invalid email format", "INVALID_EMAIL"));

            if (string.IsNullOrWhiteSpace(input.UserName))
                errors.Add(new UserError("Username is required", "USERNAME_REQUIRED"));

            if (input.UserName?.Length < 3)
                errors.Add(new UserError("Username must be at least 3 characters", "USERNAME_TOO_SHORT"));

            return errors;
        }

        private async Task<BookingValidationResult> ValidateBookingRequest(
            PropertyTenantsDbContext context,
            CreateBookingInput input,
            Guid userId,
            CancellationToken cancellationToken)
        {
            var errors = new List<UserError>();

            // Validate dates
            if (input.CheckInDate >= input.CheckOutDate)
                errors.Add(new UserError("Check-out date must be after check-in date", "INVALID_DATES"));

            if (input.CheckInDate < DateTime.Today)
                errors.Add(new UserError("Check-in date cannot be in the past", "PAST_DATE"));

            if (input.NumberOfGuests <= 0)
                errors.Add(new UserError("Number of guests must be greater than 0", "INVALID_GUEST_COUNT"));

            // Get property
            var property = await context.Properties
                .Include(p => p.PropertyDetail)
                .Include(p => p.Host)
                .FirstOrDefaultAsync(p => p.Id == input.PropertyId, cancellationToken);

            if (property == null)
            {
                errors.Add(new UserError("Property not found", "PROPERTY_NOT_FOUND"));
                return new BookingValidationResult { Errors = errors };
            }

            // Check if property is available
            if (property.Status != PropertyStatus.Available)
                errors.Add(new UserError("Property is not available for booking", "PROPERTY_NOT_AVAILABLE"));

            // Check guest capacity
            if (property.PropertyDetail != null && input.NumberOfGuests > property.PropertyDetail.MaxGuests)
                errors.Add(new UserError($"Property can accommodate maximum {property.PropertyDetail.MaxGuests} guests", "EXCEEDS_CAPACITY"));

            // Check availability (no overlapping bookings)
            var overlappingBookings = await context.Bookings
                .AnyAsync(b => b.PropertyId == input.PropertyId &&
                              b.Status == "Confirmed" &&
                              b.CheckInDate < input.CheckOutDate &&
                              b.CheckOutDate > input.CheckInDate, cancellationToken);

            if (overlappingBookings)
                errors.Add(new UserError("Property is not available for the selected dates", "DATES_NOT_AVAILABLE"));

            // Check if user is trying to book their own property
            if (property.HostId == userId)
                errors.Add(new UserError("You cannot book your own property", "CANNOT_BOOK_OWN_PROPERTY"));

            return new BookingValidationResult { Errors = errors, Property = property };
        }

        private async Task<ReviewValidationResult> ValidateReviewRequest(
            PropertyTenantsDbContext context,
            CreateReviewInput input,
            Guid userId,
            CancellationToken cancellationToken)
        {
            var errors = new List<UserError>();

            if (input.Rating < 1 || input.Rating > 5)
                errors.Add(new UserError("Rating must be between 1 and 5", "INVALID_RATING"));

            if (string.IsNullOrWhiteSpace(input.Comment))
                errors.Add(new UserError("Comment is required", "COMMENT_REQUIRED"));

            // Get booking
            var booking = await context.Bookings
                .Include(b => b.Property)
                    .ThenInclude(p => p.Host)
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.Id == input.BookingId, cancellationToken);

            if (booking == null)
            {
                errors.Add(new UserError("Booking not found", "BOOKING_NOT_FOUND"));
                return new ReviewValidationResult { Errors = errors };
            }

            // Check if user is the guest of this booking
            if (booking.GuestId != userId)
                errors.Add(new UserError("You can only review your own bookings", "UNAUTHORIZED_REVIEW"));

            // Check if booking is completed
            if (booking.Status != "Completed" && booking.CheckOutDate > DateTime.Today)
                errors.Add(new UserError("You can only review completed bookings", "BOOKING_NOT_COMPLETED"));

            // Check if review already exists
            var existingReview = await context.Reviews
                .AnyAsync(r => r.BookingId == input.BookingId && r.ReviewerId == userId, cancellationToken);

            if (existingReview)
                errors.Add(new UserError("You have already reviewed this booking", "REVIEW_EXISTS"));

            return new ReviewValidationResult 
            { 
                Errors = errors, 
                Booking = booking,
                Property = booking?.Property 
            };
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    // ========== VALIDATION RESULT TYPES ==========

    public class BookingValidationResult
    {
        public List<UserError> Errors { get; set; } = new();
        public Property? Property { get; set; }
    }

    public class ReviewValidationResult
    {
        public List<UserError> Errors { get; set; } = new();
        public Booking? Booking { get; set; }
        public Property? Property { get; set; }
    }

    // ========== PAYLOAD TYPES ==========

    public class PropertyPayload
    {
        public Property? Property { get; set; }
        public IEnumerable<UserError> Errors { get; set; } = Array.Empty<UserError>();
    }

    public class UserPayload
    {
        public User? User { get; set; }
        public IEnumerable<UserError> Errors { get; set; } = Array.Empty<UserError>();
    }

    public class BookingPayload
    {
        public Booking? Booking { get; set; }
        public IEnumerable<UserError> Errors { get; set; } = Array.Empty<UserError>();
    }

    public class ReviewPayload
    {
        public Review? Review { get; set; }
        public IEnumerable<UserError> Errors { get; set; } = Array.Empty<UserError>();
    }

    public class UserError
    {
        public UserError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }
        public string Code { get; }
    }
}
