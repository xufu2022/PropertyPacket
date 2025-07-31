﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Localized;

namespace PropertyTenants.Persistence.MappingConfigurations.Localized
{
    public class SmsMessageConfiguration : IEntityTypeConfiguration<SmsMessage>
    {
        public void Configure(EntityTypeBuilder<SmsMessage> builder)
        {
            builder.ToTable("SmsMessages");
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            builder.HasIndex(x => x.SentDateTime).IncludeProperties(x => new { x.ExpiredDateTime, x.AttemptCount, x.MaxAttemptCount, x.NextAttemptDateTime });
            //builder.HasIndex(x => x.CreatedDateTime);
        }
    }

    public class ArchivedSmsMessageConfiguration : IEntityTypeConfiguration<ArchivedSmsMessage>
    {
        public void Configure(EntityTypeBuilder<ArchivedSmsMessage> builder)
        {
            builder.ToTable("ArchivedSmsMessages");
            //builder.HasIndex(x => x.CreatedDateTime);
        }
    }
}
