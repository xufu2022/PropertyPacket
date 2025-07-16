using System.ComponentModel;
using PropertyTenants.Domain.Clients;
using PropertyTenants.Domain.Common;

namespace PropertyTenants.Domain.Assets
{
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

    public class PropertyDetail
    {
        //public PropertyDetail(Guid id) => Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.", nameof(id));
        public Guid Id { get; init; }
        public required string Description { get; set; }
        public int MaxGuests { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool HasWifi { get; set; }
        public bool HasKitchen { get; set; }
        public string[] Photos { get; set; } = [];
        public Property? Property { get; set; } 
    }

    public enum PropertyType
    {
        [Description("Apartment/Flat")]
        Apartment,
        [Description("House")]
        House,
        [Description("Secondary Unit (Guesthouse/Tiny Home)")]
        GuestHome,
        [Description("Unique Space")]
        UniqueSpace,
        [Description("Bed and Breakfast")]
        Bed,
        [Description("Boutique Hotel")]
        Hotel
    }

    public enum PropertyStatus
    {
        [Description("Entire Place")]
        EntireProperty,
        [Description("Private Room")]
        PrivateRoom,
        [Description("Shared Room")]
        SharedRoom,
        [Description("Hotel Room")]
        HotelRoom,
        [Description("Airbnb Plus")]
        AirbnbPlus,
        [Description("Airbnb Luxe")]
        AirbnbLux,
        [Description("Superhost")]
        SuperHost
    }
}
