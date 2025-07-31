using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyTenants.Domain.Entities.Clients;

namespace PropertyTenants.Persistence.MappingConfigurations.Clients
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder
                .HasMany(x => x.UserRoles)
                .WithOne(f => f.Role)
                .HasForeignKey(f => f.RoleId)
                .OnDelete(DeleteBehavior.Cascade) 
                .IsRequired();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasAlternateKey(x => x.Name);

        }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade) 
                .IsRequired();
            builder
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade) 
                .IsRequired();
        }
    }

    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");
            builder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        }
    }


}
