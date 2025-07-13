using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PropertyTenants.Infrastructure.MappingConfigurations.Store
{
    public class StoreConfiguration : IEntityTypeConfiguration<Domain.Store.Store>
    {
        public void Configure(EntityTypeBuilder<Domain.Store.Store> builder)
        {
            builder.ToTable("Stores");
            builder.HasOne(e=>e.StoreInfo).WithOne()
                .HasForeignKey<Domain.Store.StoreInfo>(e => e.StoreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
