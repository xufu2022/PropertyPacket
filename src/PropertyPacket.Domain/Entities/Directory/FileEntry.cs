using PropertyTenants.Domain.Infrastructure.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Entities.Directory
{
    public class FileEntry(Guid id) : AbstractDomain(id), IAggregateRoot, IFileEntry
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long Size { get; set; }

        public DateTimeOffset UploadedTime { get; set; }

        public string FileName { get; set; }

        public string FileLocation { get; set; }

        public bool Encrypted { get; set; }

        public string EncryptionKey { get; set; }

        public string EncryptionIV { get; set; }
    }
}
