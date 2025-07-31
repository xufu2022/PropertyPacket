using PropertyTenants.Domain.Infrastructure.Storages;
using PropertyTenants.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyTenants.Domain.Entities.Directory;

namespace PropertyTenants.Domain.Entities.Localized
{
    public class EmailMessageAttachment(Guid id) : AbstractDomain(id)
    {
        public Guid EmailMessageId { get; set; }

        public Guid FileEntryId { get; set; }

        public string Name { get; set; }

        public EmailMessage EmailMessage { get; set; }

        public FileEntry FileEntry { get; set; }
    }
    public class EmailMessage(Guid id) : EmailMessageBase(id), IAggregateRoot, IEmailMessage
    {
        public ICollection<EmailMessageAttachment> EmailMessageAttachments { get; set; }
    }

    public class ArchivedEmailMessage(Guid id) : EmailMessageBase(id), IAggregateRoot
    {
    }

    public abstract class EmailMessageBase(Guid id) : AbstractDomain(id)
    {
        public string From { get; set; }

        public string Tos { get; set; }

        public string CCs { get; set; }

        public string BCCs { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int AttemptCount { get; set; }

        public int MaxAttemptCount { get; set; }

        public DateTimeOffset? NextAttemptDateTime { get; set; }

        public DateTimeOffset? ExpiredDateTime { get; set; }

        public string Log { get; set; }

        public DateTimeOffset? SentDateTime { get; set; }

        public Guid? CopyFromId { get; set; }
    }

}
