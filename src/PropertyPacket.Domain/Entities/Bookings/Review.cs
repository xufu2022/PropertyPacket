using Microsoft.EntityFrameworkCore;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Domain.Entities.Bookings
{
    [Comment("Review managed on the website")]
    public class Review(Guid id) : AbstractDomain(id)
    {
        public Guid PropertyId { get; set; }
        public Guid BookingId { get; set; }
        public Guid ReviewerId { get; set; } // User who wrote the review
        public Guid RevieweeId { get; set; } // User being reviewed (host or guest)
        public decimal Rating { get; set; }
        public required string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual required Property Property { get; set; }
        public virtual required Booking Booking { get; set; }
        public virtual required User Reviewer { get; set; }
        public virtual required User Reviewee { get; set; }
    }
}