using PropertyPacket.Domain.Clients;
using PropertyPacket.Domain.Sites;

namespace PropertyPacket.Domain.Assets
{
    public class Booking : AbstractDomain
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

    public class Review : AbstractDomain
    {
        public Guid PropertyId { get; set; }
        public Guid BookingId { get; set; }
        public Guid ReviewerId { get; set; }
        public Decimal Rating { get; set; }
        public required string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual required Property Property { get; set; }
        public virtual required Booking Booking { get; set; }
        public virtual required User Reviewer { get; set; }
    }
}