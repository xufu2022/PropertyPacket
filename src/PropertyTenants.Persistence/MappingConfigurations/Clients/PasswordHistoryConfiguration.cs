using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Persistence.MappingConfigurations.Clients
{
    public class PasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistory>
    {
        public void Configure(EntityTypeBuilder<PasswordHistory> builder)
        {
            builder.ToTable("PasswordHistories");
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}
