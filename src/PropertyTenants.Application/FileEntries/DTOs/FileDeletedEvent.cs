using PropertyTenants.Domain.Entities.Directory;
using PropertyTenants.Domain.Infrastructure.Messaging;

namespace PropertyTenants.Application.FileEntries.DTOs;

public class FileDeletedEvent : IMessageBusEvent
{
    public FileEntry FileEntry { get; set; }
}
