using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.InputTypes;

public record CreatePropertyInput(
    string Title,
    PropertyType Type,
    PropertyStatus Status,
    decimal PricePerNight,
    Guid HostId,
    CreateAddressInput Address,
    CreatePropertyDetailInput? Detail);

public record UpdatePropertyInput(
    string? Title,
    PropertyType? Type,
    PropertyStatus? Status,
    decimal? PricePerNight,
    UpdateAddressInput? Address,
    UpdatePropertyDetailInput? Detail);

public record CreateAddressInput(
    string? Street,
    string? City,
    string? State,
    string? ZipCode,
    string? Country,
    string? Email,
    string? PhoneNumber,
    string? Mobile);

public record UpdateAddressInput(
    string? Street,
    string? City,
    string? State,
    string? ZipCode,
    string? Country,
    string? Email,
    string? PhoneNumber,
    string? Mobile);

public record CreatePropertyDetailInput(
    int? Bedrooms,
    int? Bathrooms,
    int? MaxGuests,
    decimal? Area,
    string? Description,
    string? Rules,
    string? CheckInTime,
    string? CheckOutTime);

public record UpdatePropertyDetailInput(
    int? Bedrooms,
    int? Bathrooms,
    int? MaxGuests,
    decimal? Area,
    string? Description,
    string? Rules,
    string? CheckInTime,
    string? CheckOutTime);

public record CreateBookingInput(
    Guid PropertyId,
    Guid GuestId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int NumberOfGuests);

public record UpdateBookingInput(
    DateTime? CheckInDate,
    DateTime? CheckOutDate,
    int? NumberOfGuests,
    string? Status);

public record CreateReviewInput(
    Guid PropertyId,
    Guid UserId,
    Guid? BookingId,
    int Rating,
    string? Comment);

public record CreateUserInput(
    string Username,
    string Email,
    string Password,
    string? FirstName,
    string? LastName);

public record UpdateUserInput(
    string? Username,
    string? Email,
    string? FirstName,
    string? LastName,
    bool? IsActive);

public record CreateFeatureInput(
    string Name,
    string? Description,
    string? Icon,
    int? FeatureGroupId,
    bool IsActive = true);

public record UpdateFeatureInput(
    string? Name,
    string? Description,
    string? Icon,
    int? FeatureGroupId,
    bool? IsActive);

public record AddPropertyFeatureInput(
    Guid PropertyId,
    int FeatureId,
    string? Value);
