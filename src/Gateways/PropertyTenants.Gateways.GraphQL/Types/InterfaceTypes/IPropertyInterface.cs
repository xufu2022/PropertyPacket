using PropertyTenants.Domain.Entities.Properties;

namespace PropertyTenants.Gateways.GraphQL.Types.InterfaceTypes;

[InterfaceType("Property")]
public interface IPropertyInterface
{
    Guid Id { get; }
    string Title { get; }
    PropertyType Type { get; }
    PropertyStatus Status { get; }
    decimal PricePerNight { get; }
    DateTime CreatedAt { get; }
}

public class PropertyInterfaceType : InterfaceType<IPropertyInterface>
{
    protected override void Configure(IInterfaceTypeDescriptor<IPropertyInterface> descriptor)
    {
        descriptor
            .Name("Property")
            .Description("Common interface for all property types");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier of the property")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Title)
            .Description("The title of the property")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Type)
            .Description("The type of the property")
            .Type<NonNullType<PropertyTypeEnum>>();

        descriptor
            .Field(f => f.Status)
            .Description("The current status of the property")
            .Type<NonNullType<PropertyStatusType>>();

        descriptor
            .Field(f => f.PricePerNight)
            .Description("The price per night for the property")
            .Type<NonNullType<DecimalType>>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the property was created")
            .Type<NonNullType<DateTimeType>>();
    }
}
