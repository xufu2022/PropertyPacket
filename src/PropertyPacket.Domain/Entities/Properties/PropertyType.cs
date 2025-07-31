using System.ComponentModel;

namespace PropertyTenants.Domain.Entities.Properties;

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