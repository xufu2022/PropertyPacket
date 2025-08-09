using PropertyTenants.Persistence;
using PropertyTenants.Domain.Entities.Properties;
using PropertyTenants.Domain.Entities.Bookings;
using PropertyTenants.Domain.Entities.Clients;
using PropertyTenants.Gateways.GraphQL.Types.InputTypes;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Subscriptions;

namespace PropertyTenants.Gateways.GraphQL.Types.Mutations;

[MutationType]
public class Mutation
{
    public async Task<string> TestMutationAsync()
    {
        return await Task.FromResult("Hello from enhanced GraphQL Mutation with HotChocolate!");
    }

    public async Task<bool> DeletePropertyAsync(
        Guid id,
        [Service] PropertyTenantsDbContext context,
        [Service] ITopicEventSender eventSender)
    {
        var property = await context.Properties.FindAsync(id);
        if (property == null) return false;

        context.Properties.Remove(property);
        await context.SaveChangesAsync();

        // Send subscription event
        await eventSender.SendAsync("PropertyDeleted", new { Id = id });

        return true;
    }
}
