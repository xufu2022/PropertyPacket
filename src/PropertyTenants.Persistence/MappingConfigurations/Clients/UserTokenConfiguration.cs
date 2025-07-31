using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Persistence.MappingConfigurations.Clients
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        }
    }
}
