using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyTenants.Domain.Assets;
using PropertyTenants.Domain.Clients;
using PropertyTenants.Domain.Common;

namespace PropertyTenants.Infrastructure.Seeds
{
    public class PropertyTenantsDataSeed
    {
        private static readonly List<Address> addresses = SeedAddresses();
        private static readonly List<User> users = SeedUsers();
        private static readonly List<Role> roles = SeedRoles();
        public static List<Role> SeedRoles()
        {
            return
            [
                new Role(new Guid("550e8400-e29b-41d4-a716-446655440001"))
                {
                    Name = "Admin",
                    Description = "Administrator role with full permissions"
                },

                new Role(new Guid("550e8400-e29b-41d4-a716-446655440002"))
                {
                    Name = "User",
                    Description = "Regular user role with limited permissions"
                },

                new Role(new Guid("550e8400-e29b-41d4-a716-446655440003"))
                {
                    Name = "Guest",
                    Description = "Guest role with minimal permissions"
                }
            ];
        }

        public static List<Address> SeedAddresses()
        {
            return
            [
                new Address(0, "123 Main St", "Apt 101", "New York", "USA", "10001"),
                new Address(0, "456 Elm St", "", "Los Angeles", "USA", "90001"),
                new Address(0, "789 Oak Ave", "Suite 5", "Chicago", "USA", "60601"),
                new Address(0, "321 Pine Rd", "", "Houston", "USA", "77001"),
                new Address(0, "654 Maple Dr", "Unit 3", "Miami", "USA", "33101"),
                new Address(0, "987 Cedar Ln", "", "Seattle", "USA", "98101"),
                new Address(0, "147 Birch St", "Apt 4B", "Boston", "USA", "02101"),
                new Address(0, "258 Spruce Way", "", "San Francisco", "USA", "94101"),
                new Address(0, "369 Willow Ct", "Floor 2", "Austin", "USA", "73301"),
                new Address(0, "741 Poplar Ave", "", "Denver", "USA", "80201")
            ];
        }

        public static List<User> SeedUsers()
        {
            return
            [
                new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000001"))
            {
                FriendlyName = "Alice Smith",
                ClientName = "Alice S.",
                ShortDescription = "System Administrator",
                Description = "Manages system operations and user permissions",
                UserName = "alice.smith",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X", // Placeholder bcrypt hash
                ContactInfo = new ContactInfo("alice.smith@example.com", "+1-555-0101", "+1-555-0102"),
                AddressId = 1,
                IsHost = true,
                IsGuest = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000002"))
            {
                FriendlyName = "Bob Johnson",
                ClientName = "Bob J.",
                ShortDescription = "Regular User",
                Description = "Standard user with access to core features",
                UserName = "bob.johnson",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("bob.johnson@example.com", "+1-555-0103", "+1-555-0104"),
                AddressId = 2,
                IsHost = false,
                IsGuest = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000003"))
            {
                FriendlyName = "Carol Williams",
                ClientName = "Carol W.",
                ShortDescription = "Guest User",
                Description = "Guest with limited access",
                UserName = "carol.williams",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("carol.williams@example.com", "+1-555-0105", "+1-555-0106"),
                AddressId = 3,
                IsHost = false,
                IsGuest = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000004"))
            {
                FriendlyName = "David Brown",
                ClientName = "David B.",
                ShortDescription = "System Administrator",
                Description = "Manages system configurations",
                UserName = "david.brown",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("david.brown@example.com", "+1-555-0107", "+1-555-0108"),
                AddressId = 4,
                IsHost = true,
                IsGuest = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000005"))
            {
                FriendlyName = "Emma Davis",
                ClientName = "Emma D.",
                ShortDescription = "Regular User",
                Description = "Access to standard features",
                UserName = "emma.davis",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("emma.davis@example.com", "+1-555-0109", "+1-555-0110"),
                AddressId = 5,
                IsHost = false,
                IsGuest = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000006"))
            {
                FriendlyName = "Frank Wilson",
                ClientName = "Frank W.",
                ShortDescription = "Guest User",
                Description = "Limited access guest",
                UserName = "frank.wilson",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("frank.wilson@example.com", "+1-555-0111", "+1-555-0112"),
                AddressId = 6,
                IsHost = false,
                IsGuest = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000007"))
            {
                FriendlyName = "Grace Lee",
                ClientName = "Grace L.",
                ShortDescription = "Regular User",
                Description = "Standard feature access",
                UserName = "grace.lee",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("grace.lee@example.com", "+1-555-0113", "+1-555-0114"),
                AddressId = 7,
                IsHost = false,
                IsGuest = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000008"))
            {
                FriendlyName = "Henry Taylor",
                ClientName = "Henry T.",
                ShortDescription = "System Administrator",
                Description = "Manages user roles",
                UserName = "henry.taylor",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("henry.taylor@example.com", "+1-555-0115", "+1-555-0116"),
                AddressId = 8,
                IsHost = true,
                IsGuest = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000009"))
            {
                FriendlyName = "Isabella Moore",
                ClientName = "Isabella M.",
                ShortDescription = "Regular User",
                Description = "Access to core features",
                UserName = "isabella.moore",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("isabella.moore@example.com", "+1-555-0117", "+1-555-0118"),
                AddressId = 9,
                IsHost = false,
                IsGuest = false,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User(new Guid("6b1e3b7a-4f2b-4e6e-9a1c-000000000010"))
            {
                FriendlyName = "James Anderson",
                ClientName = "James A.",
                ShortDescription = "Guest User",
                Description = "Guest with minimal access",
                UserName = "james.anderson",
                PasswordHash = "$2a$11$5z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X8k6Y8z3Y5k9Y6z2X",
                ContactInfo = new ContactInfo("james.anderson@example.com", "+1-555-0119", "+1-555-0120"),
                AddressId = 10,
                IsHost = false,
                IsGuest = true,
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
            ];
        }

        public static List<UserRole> SeedUserRoles()
        {
            return
            [
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000001"))
                {
                    UserId = users[0].Id,
                    RoleId = roles[0].Id 
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000002"))
                {
                    UserId = users[0].Id,
                    RoleId = roles[1].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000003"))
                {
                    UserId = users[0].Id,
                    RoleId = roles[2].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000004"))
                {
                    UserId = users[1].Id,
                    RoleId = roles[1].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000005"))
                {
                    UserId = users[2].Id,
                    RoleId = roles[2].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000006"))
                {
                    UserId = users[3].Id,
                    RoleId = roles[0].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000007"))
                {
                    UserId = users[4].Id,
                    RoleId = roles[1].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000008"))
                {
                    UserId = users[5].Id,
                    RoleId = roles[2].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000009"))
                {
                    UserId = users[6].Id,
                    RoleId = roles[0].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000010"))
                {
                    UserId = users[7].Id,
                    RoleId = roles[1].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000011"))
                {
                    UserId = users[8].Id,
                    RoleId = roles[2].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000012"))
                {
                    UserId = users[9].Id,
                    RoleId = roles[0].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000013"))
                {
                    UserId = users[9].Id,
                    RoleId = roles[1].Id
                },
                new UserRole(new Guid("a1b2c3d4-e5f6-47a8-9b0c-000000000014"))
                {
                    UserId = users[9].Id,
                    RoleId = roles[2].Id
                }

            ];
        }

        public static List<FeatureGroup> SeedFeatureGroups()
        {
            return
            [
                new FeatureGroup
                {
                    Id = 0, // Auto-generated by database
                    Name = "Amenities",
                    Description = "Basic amenities available in the property"
                },
                new FeatureGroup
                {
                    Id = 0,
                    Name = "Security",
                    Description = "Security features for safety"
                },
                new FeatureGroup
                {
                    Id = 0,
                    Name = "Utilities",
                    Description = "Utility services provided"
                },
                new FeatureGroup
                {
                    Id = 0,
                    Name = "Accessibility",
                    Description = "Features for accessibility"
                },
                new FeatureGroup
                {
                    Id = 0,
                    Name = "Leisure",
                    Description = "Recreational and leisure facilities"
                }
            ];
        }

        public static List<Feature> SeedFeatures()
        {
            return
            [
            new Feature
            {
                Id = 0, // Auto-generated by database
                Name = "Wi-Fi",
                Description = "High-speed wireless internet",
                FeatureGroupId = 1 // Belongs to Amenities
            },
            new Feature
            {
                Id = 0,
                Name = "Air Conditioning",
                Description = "Central air conditioning system",
                FeatureGroupId = 1 // Belongs to Amenities
            },
            new Feature
            {
                Id = 0,
                Name = "CCTV",
                Description = "24/7 surveillance cameras",
                FeatureGroupId = 2 // Belongs to Security
            },
            new Feature
            {
                Id = 0,
                Name = "Security Guard",
                Description = "On-site security personnel",
                FeatureGroupId = 2 // Belongs to Security
            },
            new Feature
            {
                Id = 0,
                Name = "Electricity",
                Description = "Reliable electricity supply",
                FeatureGroupId = 3 // Belongs to Utilities
            },
            new Feature
            {
                Id = 0,
                Name = "Water Supply",
                Description = "Continuous water supply",
                FeatureGroupId = 3 // Belongs to Utilities
            },
            new Feature
            {
                Id = 0,
                Name = "Wheelchair Access",
                Description = "Wheelchair-friendly entrances and pathways",
                FeatureGroupId = 4 // Belongs to Accessibility
            },
            new Feature
            {
                Id = 0,
                Name = "Elevator",
                Description = "Elevator for easy access to all floors",
                FeatureGroupId = 4 // Belongs to Accessibility
            },
            new Feature
            {
                Id = 0,
                Name = "Swimming Pool",
                Description = "Outdoor swimming pool",
                FeatureGroupId = 5 // Belongs to Leisure
            },
            new Feature
            {
                Id = 0,
                Name = "Gym",
                Description = "Fully equipped fitness center",
                FeatureGroupId = 5 // Belongs to Leisure
            }
            ];
        }

        // Pseudocode plan:
        // 1. Replace all direct assignments to missing Property fields (Description, MaxGuests, Bedrooms, Bathrooms, HasWifi, HasKitchen, Photos) with a new PropertyDetail object.
        // 2. Add a PropertyDetail object to each Property, setting the above fields inside PropertyDetail.
        // 3. Remove Host = null assignment if not required or set to default value if needed.

        public static List<Property> SeedProperties()
        {
            return [
                new Property(new Guid("10000000-0000-0000-0000-000000000001"))
                {
                    HostId = new Guid("6B1E3B7A-4F2B-4E6E-9A1C-000000000001"),
                    Title = "Cozy Downtown Apartment",
                    Type = PropertyType.Apartment,
                    Status = PropertyStatus.EntireProperty,
                    Address = new Address(0, "123 Main St", "Apt 101", "New York", "USA", "10001"),
                    PricePerNight = 150.00m,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    LastUpdatedAt = null,
                    Timestamp = new byte[0],
                    PropertyDetail = new PropertyDetail
                    {
                        Id = new Guid("10000000-0000-0000-0000-000000000001"),
                        Description = "A modern apartment in the heart of New York with stunning views.",
                        MaxGuests = 4,
                        Bedrooms = 2,
                        Bathrooms = 1,
                        HasWifi = true,
                        HasKitchen = true,
                        Photos = new string[]
                        {
                            "photo1.jpg",
                            "photo2.jpg"
                        }
                    }
                },
                new Property(new Guid("10000000-0000-0000-0000-000000000002"))
                {
                    HostId = new Guid("6B1E3B7A-4F2B-4E6E-9A1C-000000000001"),
                    Title = "Beachfront House",
                    Type = PropertyType.House,
                    Status = PropertyStatus.EntireProperty,
                    Address = new Address(0, "456 Elm St", "", "Los Angeles", "USA", "90001"),
                    PricePerNight = 250.00m,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    LastUpdatedAt = null,
                    Timestamp = new byte[0],
                    PropertyDetail = new PropertyDetail()
                    {
                        Id = new Guid("10000000-0000-0000-0000-000000000002"),
                        Description = "Spacious house with direct beach access in Los Angeles.",
                        MaxGuests = 6,
                        Bedrooms = 3,
                        Bathrooms = 2,
                        HasWifi = true,
                        HasKitchen = true,
                        Photos = new string[] { "beach1.jpg", "beach2.jpg" }
                    }
                },
                new Property(new Guid("10000000-0000-0000-0000-000000000003"))
                {
                    HostId = new Guid("6B1E3B7A-4F2B-4E6E-9A1C-000000000001"),
                    Title = "Chic Loft in Chicago",
                    Type = PropertyType.Apartment,
                    Status = PropertyStatus.AirbnbPlus,
                    Address = new Address(0, "789 Oak Ave", "Suite 5", "Chicago", "USA", "60601"),
                    PricePerNight = 180.00m,
                    CreatedAt = DateTime.UtcNow.AddDays(-20),
                    LastUpdatedAt = null,
                    Timestamp = new byte[0],
                    PropertyDetail = new PropertyDetail
                    {
                        Id = new Guid("10000000-0000-0000-0000-000000000003"),
                        Description = "Stylish loft with modern amenities in downtown Chicago.",
                        MaxGuests = 3,
                        Bedrooms = 1,
                        Bathrooms = 1,
                        HasWifi = true,
                        HasKitchen = false,
                        Photos = new string[] { "loft1.jpg" }
                    }
                },
                new Property(new Guid("10000000-0000-0000-0000-000000000004"))
                {
                    HostId = new Guid("6B1E3B7A-4F2B-4E6E-9A1C-000000000001"),
                    Title = "Rustic Guest House",
                    Type = PropertyType.GuestHome,
                    Status = PropertyStatus.EntireProperty,
                    Address = new Address(0, "321 Pine Rd", "", "Houston", "USA", "77001"),
                    PricePerNight = 100.00m,
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    LastUpdatedAt = null,
                    Timestamp = new byte[0],
                    PropertyDetail = new PropertyDetail
                    {
                        Id = new Guid("10000000-0000-0000-0000-000000000004"),
                        Description = "Charming guesthouse in Houston with a private garden.",
                        MaxGuests = 2,
                        Bedrooms = 1,
                        Bathrooms = 1,
                        HasWifi = true,
                        HasKitchen = true,
                        Photos = new string[] { "guest1.jpg", "guest2.jpg" }
                    }
                },
                new Property(new Guid("10000000-0000-0000-0000-000000000005"))
                {
                    HostId = new Guid("6B1E3B7A-4F2B-4E6E-9A1C-000000000001"),
                    Title = "Luxury Condo in Miami",
                    Type = PropertyType.Apartment,
                    Status = PropertyStatus.AirbnbLux,
                    Address = new Address(0, "654 Maple Dr", "Unit 3", "Miami", "USA", "33101"),
                    PricePerNight = 300.00m,
                    CreatedAt = DateTime.UtcNow.AddDays(-12),
                    LastUpdatedAt = null,
                    Timestamp = new byte[0],
                    PropertyDetail = new PropertyDetail
                    {
                        Id = new Guid("10000000-0000-0000-0000-000000000005"),
                        Description = "Upscale condo with ocean views and premium amenities.",
                        MaxGuests = 5,
                        Bedrooms = 2,
                        Bathrooms = 2,
                        HasWifi = true,
                        HasKitchen = true,
                        Photos = new string[] { "condo1.jpg", "condo2.jpg", "condo3.jpg" }
                    }
                },
                // ... repeat for all other properties, moving the problematic fields into PropertyDetail ...
            ];
        }

        public static List<PropertyFeature> SeedPropertyFeatures()
        {
            return
            [
                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000001"),
                    FeatureId = 1 // Wi-Fi
                },
                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000001"),
                    FeatureId = 2 // Air Conditioning
                },

                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000001"),
                    FeatureId = 3 // CCTV
                },
                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000001"),
                    FeatureId = 4 // Security Guard
                },
                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000001"),
                    FeatureId = 5 // Electricity
                },
                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000001"),
                    FeatureId = 6 // Water Supply
                },
                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000001"),
                    FeatureId = 7 // Wheelchair Access
                },
                new PropertyFeature
                {
                    PropertyId = new Guid("10000000-0000-0000-0000-000000000004"),
                    FeatureId = 8 // Elevator
                }
            ];
        }

    }
}
