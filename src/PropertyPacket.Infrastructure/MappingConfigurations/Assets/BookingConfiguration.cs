using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Assets;
using PropertyTenants.Domain.Common;

namespace PropertyTenants.Infrastructure.MappingConfigurations.Assets
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasOne(a=>a.Guest)
                .WithMany()
                .HasForeignKey(p => p.GuestId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
        }
    }

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews"); //.ToTable("Reviews", schema: "dbo"); // t => t.ExcludeFromMigrations() do not allow changes to this
            //tableBuilder => tableBuilder.HasComment("Reviews managed on the website")
            builder.HasOne(r => r.Property)
                .WithMany() // Property can have many reviews
                .HasForeignKey(r => r.PropertyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Booking)
                .WithMany()
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne(r => r.Reviewer)
                .WithMany() // User can write many reviews
                .HasForeignKey(r => r.ReviewerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Reviewee)
                .WithMany() // User can receive many reviews
                .HasForeignKey(r => r.RevieweeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Rating)//.HasColumnType("decimal(5,2)");
                .HasPrecision(5, 2);

        }
    }
}
