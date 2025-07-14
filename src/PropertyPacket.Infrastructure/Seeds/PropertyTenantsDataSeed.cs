using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyTenants.Domain.Clients;

namespace PropertyTenants.Infrastructure.Seeds
{
    public class PropertyTenantsDataSeed
    {
        public static List<Role> SeedRolesAndUsers()
        {
            List<Role> list =
            [
                new Role
                {
                    Id = Guid.Parse("4b9e4e6b-1b2c-4d5e-8f7a-9c8d7e6f5a4b"), Name = "Host",
                    Description = "Manages properties and hosts guests."
                },
                new Role
                {
                    Id = Guid.Parse("3c8d3d7a-2c3d-4e6f-9g8b-0d9e8f7g6b5c"), Name = "Guest",
                    Description = "Books and stays at properties."
                },
                new Role
                {
                    Id = Guid.Parse("2d7e2e6c-3d4e-5f7g-0h9c-1e0f9g8h7c6d"), Name = "Admin",
                    Description = "Manages platform, users, and disputes."
                },
                new Role
                {
                    Id = Guid.Parse("1e6f1f7d-4e5f-6g8h-1i0d-2f1g0h9i8d7e"), Name = "SuperHost",
                    Description = "Elite host with exceptional ratings and reliability."
                },
                new Role
                {
                    Id = Guid.Parse("0f5g0g8e-5f6g-7h9i-2j1e-3g2h1i0j9e8f"), Name = "Moderator",
                    Description = "Oversees reviews and community standards."
                },
                new Role
                {
                    Id = Guid.Parse("9g4h9i0f-6g7h-8i0j-3k2f-4h3i2j1k0f9g"), Name = "PropertyManager",
                    Description = "Manages multiple properties on behalf of owners."
                },
                new Role
                {
                    Id = Guid.Parse("8h3i8j0g-7h8i-9j0k-4l3g-5i4j3k2l1g0h"), Name = "SupportAgent",
                    Description = "Handles customer support and inquiries."
                },
                new Role
                {
                    Id = Guid.Parse("7i2j7k1h-8i9j-0k1l-5m4h-6j5k4l3m2h1i"), Name = "BookingCoordinator",
                    Description = "Assists with booking logistics and scheduling."
                },
                new Role
                {
                    Id = Guid.Parse("6j1k6l2i-9j0k-1l2m-6n5i-7k6l5m4n3i2j"), Name = "MarketingSpecialist",
                    Description = "Promotes listings and platform campaigns."
                },
                new Role
                {
                    Id = Guid.Parse("5k0l5m3j-0k1l-2m3n-7o6j-8l7m6n5o4j3k"), Name = "MaintenanceCrew",
                    Description = "Handles property maintenance and inspections."
                }
            ];





            return list;


        }
    }

    //generate seed data for roles and users
    //public static class PropertyTen
    //    public static void SeedRolesAndUsers(this ModelBuilder modelBuilder)


}
