using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Persistence;
using HotChocolate.Types.Relay;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .Name("User")
            .Description("A user in the system")
            .ImplementsNode()
            .IdField(t => t.Id)
            .ResolveNode((ctx, id) => 
                ctx.DataLoader<UserByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

        descriptor
            .Field(f => f.Id)
            .ID(nameof(User))
            .Description("The unique identifier of the user");

        descriptor
            .Field(f => f.Username)
            .Description("The username of the user")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Email)
            .Description("The email address of the user")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.FirstName)
            .Description("The first name of the user")
            .Type<StringType>();

        descriptor
            .Field(f => f.LastName)
            .Description("The last name of the user")
            .Type<StringType>();

        descriptor
            .Field(f => f.IsActive)
            .Description("Whether the user is active")
            .Type<NonNullType<BooleanType>>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the user was created")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.LastUpdatedAt)
            .Description("When the user was last updated")
            .Type<DateTimeType>();

        // Hide sensitive fields
        descriptor
            .Field(f => f.Password)
            .Ignore();

        descriptor
            .Field(f => f.Salt)
            .Ignore();

        // Computed fields
        descriptor
            .Field("fullName")
            .Description("The full name of the user")
            .Type<StringType>()
            .Resolve(context =>
            {
                var user = context.Parent<User>();
                return $"{user.FirstName} {user.LastName}".Trim();
            });

        // Related data
        descriptor
            .Field("hostedProperties")
            .Description("Properties hosted by this user")
            .ResolveWith<UserResolvers>(r => r.GetHostedPropertiesAsync(default!, default!, default!))
            .UsePaging<PropertyType>()
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field("bookings")
            .Description("Bookings made by this user")
            .ResolveWith<UserResolvers>(r => r.GetBookingsAsync(default!, default!, default!))
            .UsePaging<BookingType>()
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field("reviews")
            .Description("Reviews written by this user")
            .ResolveWith<UserResolvers>(r => r.GetReviewsAsync(default!, default!, default!))
            .UsePaging<ReviewType>()
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field("roles")
            .Description("Roles assigned to this user")
            .ResolveWith<UserResolvers>(r => r.GetRolesAsync(default!, default!, default!))
            .Type<ListType<UserRoleType>>();
    }
}

public class UserResolvers
{
    public async Task<IEnumerable<Property>> GetHostedPropertiesAsync(
        [Parent] User user,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Properties
            .Where(p => p.HostId == user.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync(
        [Parent] User user,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Bookings
            .Where(b => b.GuestId == user.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Review>> GetReviewsAsync(
        [Parent] User user,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Reviews
            .Where(r => r.UserId == user.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserRole>> GetRolesAsync(
        [Parent] User user,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Include(ur => ur.Role)
            .ToListAsync(cancellationToken);
    }
}
