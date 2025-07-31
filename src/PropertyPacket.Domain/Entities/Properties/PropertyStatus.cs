using System.ComponentModel;

namespace PropertyTenants.Domain.Entities.Properties
{
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
