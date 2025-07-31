using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Store;

namespace PropertyTenants.Persistence.MappingConfigurations.Store
{
    public class StoreConfiguration : IEntityTypeConfiguration<Domain.Entities.Store.Store>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Store.Store> builder)
        {
            builder.ToTable("Stores");
            builder.HasOne(e=>e.StoreInfo).WithOne()
                .HasForeignKey<StoreInfo>(e => e.StoreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
