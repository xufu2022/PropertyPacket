using Microsoft.EntityFrameworkCore;
using PropertyTenants.Domain.Clients;

namespace PropertyTenants.Domain.Assets
{
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

    [Comment("Review managed on the website")]
    public class Review(Guid id) : AbstractDomain(id)
    {
        public Guid PropertyId { get; set; }
        public Guid BookingId { get; set; }
        public Guid ReviewerId { get; set; } // User who wrote the review
        public Guid RevieweeId { get; set; } // User being reviewed (host or guest)
        public Decimal Rating { get; set; }
        public required string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual required Property Property { get; set; }
        public virtual required Booking Booking { get; set; }
        public virtual required User Reviewer { get; set; }
        public virtual required User Reviewee { get; set; }
    }
}