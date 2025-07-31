using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Domain.Entities.Bookings;

public class Booking(Guid id) : AbstractDomain(id)
{
    public Guid PropertyId { get; set; }
    public Guid GuestId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; }
    public virtual required Property Property { get; set; }
    public virtual required User Guest { get; set; }
    public virtual required Review Review { get; set; }
}