using PropertyTenants.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Entities.Localized
{
    public class SmsMessage(Guid id) : SmsMessageBase(id), IAggregateRoot, ISmsMessage
    {
    }

    public class ArchivedSmsMessage(Guid id) : SmsMessageBase(id), IAggregateRoot
    {
    }

    public abstract class SmsMessageBase(Guid id) : AbstractDomain(id)
    {
        public string Message { get; set; }

        public string PhoneNumber { get; set; }

        public int AttemptCount { get; set; }

        public int MaxAttemptCount { get; set; }

        public DateTimeOffset? NextAttemptDateTime { get; set; }

        public DateTimeOffset? ExpiredDateTime { get; set; }

        public string Log { get; set; }

        public DateTimeOffset? SentDateTime { get; set; }

        public Guid? CopyFromId { get; set; }
    }
}
