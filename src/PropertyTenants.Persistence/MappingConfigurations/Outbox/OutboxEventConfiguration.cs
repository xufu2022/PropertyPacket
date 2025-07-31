using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Persistence.MappingConfigurations.Outbox
{
    public class OutboxEventConfiguration : IEntityTypeConfiguration<OutboxEvent>
    {
        public void Configure(EntityTypeBuilder<OutboxEvent> builder)
        {
            builder.ToTable("OutboxEvents");
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasIndex(x => new { x.Published, x.CreatedDateTime });
            builder.HasIndex(x => x.CreatedDateTime);
        }
    }

    public class ArchivedOutboxEventConfiguration : IEntityTypeConfiguration<ArchivedOutboxEvent>
    {
        public void Configure(EntityTypeBuilder<ArchivedOutboxEvent> builder)
        {
            builder.ToTable("ArchivedOutboxEvents");
            builder.HasIndex(x => x.CreatedDateTime);
        }
    }
}
