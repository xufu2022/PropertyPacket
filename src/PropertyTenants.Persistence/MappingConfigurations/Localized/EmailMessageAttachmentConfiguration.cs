using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Localized;

namespace PropertyTenants.Persistence.MappingConfigurations.Localized
{
    public class EmailMessageAttachmentConfiguration : IEntityTypeConfiguration<EmailMessageAttachment>
    {
        public void Configure(EntityTypeBuilder<EmailMessageAttachment> builder)
        {
            builder.ToTable("EmailMessageAttachments");
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}
