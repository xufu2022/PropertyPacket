using PropertyTenants.Domain.Entities.Assets;
using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.UnionTypes;

[UnionType("PropertyUnion")]
public abstract class PropertyUnion
{
    public static PropertyUnion From(Property property) => property.Type switch
    {
        PropertyType.Apartment => new ApartmentUnion(),
        PropertyType.House => new HouseUnion(),
        PropertyType.Hotel => new HotelUnion(),
        PropertyType.GuestHome => new GuestHomeUnion(),
        PropertyType.UniqueSpace => new UniqueSpaceUnion(),
        PropertyType.Bed => new BedUnion(),
        _ => throw new ArgumentException("Unknown property type")
    };
}

public class ApartmentUnion : PropertyUnion { }
public class HouseUnion : PropertyUnion { }
public class HotelUnion : PropertyUnion { }
public class GuestHomeUnion : PropertyUnion { }
public class UniqueSpaceUnion : PropertyUnion { }
public class BedUnion : PropertyUnion { }

public class PropertyUnionType : UnionType<PropertyUnion>
{
    protected override void Configure(IUnionTypeDescriptor<PropertyUnion> descriptor)
    {
        descriptor
            .Name("PropertyUnion")
            .Description("Union of all property types");

        descriptor.Type<PropertyTenants.Gateways.GraphQL.Types.ObjectTypes.PropertyType>();
    }
}
