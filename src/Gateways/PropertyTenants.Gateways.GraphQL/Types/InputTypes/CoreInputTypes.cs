namespace PropertyTenants.Gateways.GraphQL.Types.InputTypes;

[InputObjectType]
public class CreatePropertyInput
{
    public required string Title { get; set; }
    public decimal PricePerNight { get; set; }
    public Guid HostId { get; set; }
}

[InputObjectType]
public class CreateBookingInput
{
    public Guid PropertyId { get; set; }
    public Guid GuestId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
}

[InputObjectType]
public class CreateUserInput
{
    public required string FriendlyName { get; set; }
    public required string ClientName { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public bool IsHost { get; set; }
    public bool IsGuest { get; set; }
}
