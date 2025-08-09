namespace PropertyTenants.Gateways.GraphQL.Types.InterfaceTypes;

[InterfaceType("Auditable")]
public interface IAuditableInterface
{
    DateTime CreatedAt { get; }
    DateTime? LastUpdatedAt { get; }
    byte[] Timestamp { get; }
}

public class AuditableInterfaceType : InterfaceType<IAuditableInterface>
{
    protected override void Configure(IInterfaceTypeDescriptor<IAuditableInterface> descriptor)
    {
        descriptor
            .Name("Auditable")
            .Description("Interface for entities that support auditing");

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the entity was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.LastUpdatedAt)
            .Description("When the entity was last updated")
            .Type<DateTimeType>();

        descriptor
            .Field(f => f.Timestamp)
            .Description("Concurrency timestamp")
            .Type<NonNullType<ByteArrayType>>();
    }
}
