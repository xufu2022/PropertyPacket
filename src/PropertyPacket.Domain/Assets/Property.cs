using PropertyPacket.Domain.Clients;
using PropertyPacket.Domain.Common;
using PropertyPacket.Domain.Sites;
using System.ComponentModel;

namespace PropertyPacket.Domain.Assets
{
    public class Property: AbstractDomain
    {
        public Guid HostId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; }
        public required Address Address { get; set; }
        public decimal PricePerNight { get; set; }
        public int MaxGuests { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool HasWifi { get; set; }
        public bool HasKitchen { get; set; }
        public string[] Photos { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedAt { get; set; }
        public virtual required User Host { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
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
