using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Store;

namespace PropertyTenants.Infrastructure.MappingConfigurations.Store
{
    public class StoreInfoConfiguration : IEntityTypeConfiguration<StoreInfo>
    {
        public void Configure(EntityTypeBuilder<StoreInfo> builder)
        {
            builder.ComplexProperty(e => e.AddressInfo, b =>
            {
                b.Property(e => e.Line1).HasColumnName("Line1");
                b.Property(e => e.Line2).HasColumnName("Line2");
                b.Property(e => e.City).HasColumnName("City");
                b.Property(e => e.Country).HasColumnName("Country");
                b.Property(e => e.PostCode).HasColumnName("PostCode");
            });
        }
    }
}
