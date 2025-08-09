using PropertyTenants.Domain.Entities.Outbox;
using PropertyTenants.Domain.Entities.Localized;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class OutboxEventType : ObjectType<OutboxEvent>
{
    protected override void Configure(IObjectTypeDescriptor<OutboxEvent> descriptor)
    {
        descriptor
            .Name("OutboxEvent")
            .Description("An outbox event for eventual consistency");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.EventType)
            .Description("The type of event")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.ObjectId)
            .Description("The ID of the object this event relates to")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Payload)
            .Description("The event payload")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.IsProcessed)
            .Description("Whether the event has been processed")
            .Type<NonNullType<BooleanType>>();

        descriptor
            .Field(f => f.ProcessedAt)
            .Description("When the event was processed")
            .Type<DateTimeType>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the event was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.ActivityId)
            .Description("The activity ID for tracing")
            .Type<NonNullType<StringType>>();

        // Hide sensitive fields
        descriptor
            .Field(f => f.RowVersion)
            .Ignore();
    }
}

[ObjectType]
public class SmsMessageType : ObjectType<SmsMessage>
{
    protected override void Configure(IObjectTypeDescriptor<SmsMessage> descriptor)
    {
        descriptor
            .Name("SmsMessage")
            .Description("An SMS message");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Message)
            .Description("The message content")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.PhoneNumber)
            .Description("The recipient phone number")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.IsSent)
            .Description("Whether the message was sent")
            .Type<NonNullType<BooleanType>>();

        descriptor
            .Field(f => f.SentAt)
            .Description("When the message was sent")
            .Type<DateTimeType>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the message was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.Log)
            .Description("Delivery log")
            .Type<StringType>();
    }
}

[ObjectType]
public class EmailMessageType : ObjectType<EmailMessage>
{
    protected override void Configure(IObjectTypeDescriptor<EmailMessage> descriptor)
    {
        descriptor
            .Name("EmailMessage")
            .Description("An email message");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Subject)
            .Description("The email subject")
            .Type<StringType>();

        descriptor
            .Field(f => f.Body)
            .Description("The email body")
            .Type<StringType>();

        descriptor
            .Field(f => f.To)
            .Description("The recipient email address")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.From)
            .Description("The sender email address")
            .Type<StringType>();

        descriptor
            .Field(f => f.IsSent)
            .Description("Whether the email was sent")
            .Type<NonNullType<BooleanType>>();

        descriptor
            .Field(f => f.SentAt)
            .Description("When the email was sent")
            .Type<DateTimeType>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the email was created")
            .Type<NonNullType<DateTimeType>>();
    }
}

[ObjectType]
public class AuditLogEntryType : ObjectType<AuditLogEntry>
{
    protected override void Configure(IObjectTypeDescriptor<AuditLogEntry> descriptor)
    {
        descriptor
            .Name("AuditLogEntry")
            .Description("An audit log entry");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.UserId)
            .Description("The user who performed the action")
            .Type<UuidType>();

        descriptor
            .Field(f => f.Action)
            .Description("The action performed")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.EntityType)
            .Description("The type of entity affected")
            .Type<StringType>();

        descriptor
            .Field(f => f.EntityId)
            .Description("The ID of the entity affected")
            .Type<StringType>();

        descriptor
            .Field(f => f.OldValues)
            .Description("The old values before the change")
            .Type<StringType>();

        descriptor
            .Field(f => f.NewValues)
            .Description("The new values after the change")
            .Type<StringType>();

        descriptor
            .Field(f => f.Timestamp)
            .Description("When the action was performed")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.IpAddress)
            .Description("The IP address of the user")
            .Type<StringType>();

        descriptor
            .Field(f => f.UserAgent)
            .Description("The user agent of the client")
            .Type<StringType>();
    }
}
