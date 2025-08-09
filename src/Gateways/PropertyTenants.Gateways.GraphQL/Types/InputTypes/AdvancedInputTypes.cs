using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.InputTypes
{
    // ========== PROPERTY INPUT TYPES ==========

    public class CreatePropertyInput
    {
        public string Title { get; set; } = string.Empty;
        public PropertyType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public CreateAddressInput Address { get; set; } = null!;
        public CreatePropertyDetailInput? PropertyDetail { get; set; }
        public List<Guid>? FeatureIds { get; set; }
    }

    public class UpdatePropertyInput
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public PropertyType? Type { get; set; }
        public PropertyStatus? Status { get; set; }
        public decimal? PricePerNight { get; set; }
        public UpdateAddressInput? Address { get; set; }
        public UpdatePropertyDetailInput? PropertyDetail { get; set; }
        public List<Guid>? FeatureIds { get; set; }
    }

    public class CreateAddressInput
    {
        public string Line1 { get; set; } = string.Empty;
        public string? Line2 { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
    }

    public class UpdateAddressInput
    {
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostCode { get; set; }
    }

    public class CreatePropertyDetailInput
    {
        public string Description { get; set; } = string.Empty;
        public int MaxGuests { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int? SquareFeet { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public string? HouseRules { get; set; }
        public string? Amenities { get; set; }
    }

    public class UpdatePropertyDetailInput
    {
        public string? Description { get; set; }
        public int? MaxGuests { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? SquareFeet { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string? HouseRules { get; set; }
        public string? Amenities { get; set; }
    }

    // ========== USER INPUT TYPES ==========

    public class CreateUserInput
    {
        public string FriendlyName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Mobile { get; set; }
        public bool IsHost { get; set; }
        public bool IsGuest { get; set; } = true;
        public string? AzureAdB2CUserId { get; set; }
    }

    public class UpdateUserInput
    {
        public string? FriendlyName { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public UpdateContactInfoInput? ContactInfo { get; set; }
    }

    public class UpdateContactInfoInput
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Mobile { get; set; }
    }

    // ========== BOOKING INPUT TYPES ==========

    public class CreateBookingInput
    {
        public Guid PropertyId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
    }

    public class UpdateBookingInput
    {
        public Guid Id { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? NumberOfGuests { get; set; }
        public string? SpecialRequests { get; set; }
        public string? Status { get; set; }
    }

    public class BookingSearchInput
    {
        public Guid? PropertyId { get; set; }
        public Guid? GuestId { get; set; }
        public DateTime? CheckInFrom { get; set; }
        public DateTime? CheckInTo { get; set; }
        public DateTime? CheckOutFrom { get; set; }
        public DateTime? CheckOutTo { get; set; }
        public string? Status { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinGuests { get; set; }
        public int? MaxGuests { get; set; }
    }

    // ========== REVIEW INPUT TYPES ==========

    public class CreateReviewInput
    {
        public Guid PropertyId { get; set; }
        public Guid BookingId { get; set; }
        public int Rating { get; set; } // 1-5 stars
        public string Comment { get; set; } = string.Empty;
        public ReviewCategoryRatingsInput? CategoryRatings { get; set; }
    }

    public class UpdateReviewInput
    {
        public Guid Id { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public ReviewCategoryRatingsInput? CategoryRatings { get; set; }
        public string? Response { get; set; } // Host response to review
    }

    public class ReviewCategoryRatingsInput
    {
        public int? Cleanliness { get; set; }
        public int? Communication { get; set; }
        public int? CheckIn { get; set; }
        public int? Accuracy { get; set; }
        public int? Location { get; set; }
        public int? Value { get; set; }
    }

    public class ReviewSearchInput
    {
        public Guid? PropertyId { get; set; }
        public Guid? ReviewerId { get; set; }
        public Guid? RevieweeId { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string? SearchText { get; set; }
    }

    // ========== FEATURE INPUT TYPES ==========

    public class CreateFeatureInput
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsEssential { get; set; }
        public bool IsPopular { get; set; }
    }

    public class UpdateFeatureInput
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? Category { get; set; }
        public bool? IsEssential { get; set; }
        public bool? IsPopular { get; set; }
    }

    // ========== SEARCH AND FILTER INPUT TYPES ==========

    public class PropertySearchInput
    {
        public string? SearchText { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? RadiusKm { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? Guests { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public PropertyType? Type { get; set; }
        public PropertyStatus? Status { get; set; }
        public int? MinBedrooms { get; set; }
        public int? MinBathrooms { get; set; }
        public List<Guid>? RequiredFeatures { get; set; }
        public List<string>? Amenities { get; set; }
        public decimal? MinRating { get; set; }
        public bool? InstantBook { get; set; }
        public bool? SuperHost { get; set; }
    }

    public class UserSearchInput
    {
        public string? SearchText { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public bool? IsHost { get; set; }
        public bool? IsGuest { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    // ========== SORTING INPUT TYPES ==========

    public class PropertySortInput
    {
        public PropertySortField Field { get; set; } = PropertySortField.CreatedAt;
        public SortDirection Direction { get; set; } = SortDirection.Desc;
    }

    public class UserSortInput
    {
        public UserSortField Field { get; set; } = UserSortField.CreatedAt;
        public SortDirection Direction { get; set; } = SortDirection.Desc;
    }

    public class BookingSortInput
    {
        public BookingSortField Field { get; set; } = BookingSortField.BookedAt;
        public SortDirection Direction { get; set; } = SortDirection.Desc;
    }

    public class ReviewSortInput
    {
        public ReviewSortField Field { get; set; } = ReviewSortField.CreatedAt;
        public SortDirection Direction { get; set; } = SortDirection.Desc;
    }

    // ========== PAGINATION INPUT TYPES ==========

    public class PaginationInput
    {
        public int? First { get; set; }
        public string? After { get; set; }
        public int? Last { get; set; }
        public string? Before { get; set; }
    }

    // ========== ANALYTICS INPUT TYPES ==========

    public class AnalyticsFilterInput
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? GroupBy { get; set; } // Day, Week, Month, Year
        public List<Guid>? PropertyIds { get; set; }
        public List<Guid>? UserIds { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public PropertyType? PropertyType { get; set; }
    }

    public class RevenueAnalyticsInput
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Currency { get; set; } = "USD";
        public string? GroupBy { get; set; } // Day, Week, Month, Quarter, Year
        public List<Guid>? PropertyIds { get; set; }
        public bool IncludeTaxes { get; set; } = true;
        public bool IncludeFees { get; set; } = true;
    }

    // ========== NOTIFICATION INPUT TYPES ==========

    public class CreateNotificationInput
    {
        public Guid? UserId { get; set; } // null for global notifications
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "Info"; // Info, Warning, Error, Success
        public string? ActionUrl { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    // ========== ENUM TYPES ==========

    public enum PropertySortField
    {
        CreatedAt,
        Title,
        PricePerNight,
        Rating,
        PopularityScore,
        LastUpdatedAt
    }

    public enum UserSortField
    {
        CreatedAt,
        FriendlyName,
        Email,
        LastLoginAt
    }

    public enum BookingSortField
    {
        BookedAt,
        CheckInDate,
        CheckOutDate,
        TotalPrice,
        Status
    }

    public enum ReviewSortField
    {
        CreatedAt,
        Rating,
        PropertyTitle,
        ReviewerName
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }

    public enum AnalyticsMetricType
    {
        Views,
        Bookings,
        Revenue,
        AverageRating,
        OccupancyRate,
        ConversionRate,
        CancellationRate,
        ResponseRate,
        ResponseTime
    }
}
