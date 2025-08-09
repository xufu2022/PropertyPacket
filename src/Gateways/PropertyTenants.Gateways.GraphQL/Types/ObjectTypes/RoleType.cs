using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Persistence;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class RoleType : ObjectType<Role>
{
    protected override void Configure(IObjectTypeDescriptor<Role> descriptor)
    {
        descriptor
            .Name("Role")
            .Description("A user role in the system");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier of the role")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Type)
            .Description("The type of the role")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Value)
            .Description("The value of the role")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field("userCount")
            .Description("Number of users with this role")
            .Type<NonNullType<IntType>>()
            .ResolveWith<RoleResolvers>(r => r.GetUserCountAsync(default!, default!, default!));
    }
}

[ObjectType]
public class UserRoleType : ObjectType<UserRole>
{
    protected override void Configure(IObjectTypeDescriptor<UserRole> descriptor)
    {
        descriptor
            .Name("UserRole")
            .Description("Association between a user and a role");

        descriptor
            .Field(f => f.UserId)
            .Description("The user ID")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.RoleId)
            .Description("The role ID")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.User)
            .Description("The user")
            .ResolveWith<UserRoleResolvers>(r => r.GetUserAsync(default!, default!, default!))
            .Type<NonNullType<UserType>>();

        descriptor
            .Field(f => f.Role)
            .Description("The role")
            .ResolveWith<UserRoleResolvers>(r => r.GetRoleAsync(default!, default!, default!))
            .Type<NonNullType<RoleType>>();
    }
}

public class RoleResolvers
{
    public async Task<int> GetUserCountAsync(
        [Parent] Role role,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.UserRoles
            .CountAsync(ur => ur.RoleId == role.Id, cancellationToken);
    }
}

public class UserRoleResolvers
{
    public async Task<User> GetUserAsync(
        [Parent] UserRole userRole,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstAsync(u => u.Id == userRole.UserId, cancellationToken);
    }

    public async Task<Role> GetRoleAsync(
        [Parent] UserRole userRole,
        [Service] PropertyTenantsDbContext context,
        CancellationToken cancellationToken)
    {
        return await context.Roles
            .FirstAsync(r => r.Id == userRole.RoleId, cancellationToken);
    }
}
