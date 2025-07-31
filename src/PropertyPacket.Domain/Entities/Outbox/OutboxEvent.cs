using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Entities.Outbox
{
    public class OutboxEvent(Guid id) : OutboxEventBase(id), IAggregateRoot
    {
    }

    public class ArchivedOutboxEvent(Guid id) : OutboxEventBase(id), IAggregateRoot
    {
    }

    public abstract class OutboxEventBase(Guid id) : AbstractDomain(id)
    {
        public string EventType { get; set; }

        public Guid TriggeredById { get; set; }

        public string ObjectId { get; set; }

        public string Payload { get; set; }

        public bool Published { get; set; }

        public string ActivityId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public DateTimeOffset? UpdatedDateTime { get; set; }
    }
}
