using PropertyTenants.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace PropertyTenants.Domain.Entities.Clients;

public class AuditLogEntry(Guid id) : AbstractDomain(id), IAggregateRoot{

    public Guid UserId { get; set; }

    public string Action { get; set; }

    public string ObjectId { get; set; }

    public string Log { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    public DateTimeOffset CreatedDateTime { get; set; }

    public DateTimeOffset? UpdatedDateTime { get; set; }
}
