using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.EnumTypes;

[EnumType]
public enum PropertyStatusEnum
{
    Available,
    Booked,
    Maintenance,
    Inactive
}

[EnumType]
public enum PropertyTypeEnum
{
    Apartment,
    House,
    Condo,
    Villa,
    Studio,
    Loft
}
