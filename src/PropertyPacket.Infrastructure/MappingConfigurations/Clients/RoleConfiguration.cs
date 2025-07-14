using Microsoft.EntityFrameworkCore;
using PropertyTenants.Domain.Assets;
using PropertyTenants.Domain.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PropertyTenants.Infrastructure.MappingConfigurations.Clients
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


}
