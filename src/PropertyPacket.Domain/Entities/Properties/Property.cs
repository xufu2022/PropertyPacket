using PropertyTenants.Domain.Entities.Assets;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Domain.Entities.Common;
using PropertyTenants.Domain.Entities.Features;

namespace PropertyTenants.Domain.Entities.Properties;

public class Property(Guid id) : AbstractDomain(id)
{
    public Guid HostId { get; set; }
    public required string Title { get; set; }
    public PropertyType Type { get; set; }
    public PropertyStatus Status { get; set; }
    public required Address Address { get; set; }
    public decimal PricePerNight { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
    public byte[] Timestamp { get; set; } = null!;
    public virtual User? Host { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; } = [];
    public virtual ICollection<Review> Reviews { get; set; } = [];
    public virtual ICollection<PropertyFeature> PropertyFeatures { get; set; } = [];
    public virtual PropertyDetail? PropertyDetail { get; set; } // Navigation to PropertyDetail
}