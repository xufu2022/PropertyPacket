using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Gateways.GraphQL.Types.Mutations
{
    public class Mutation
    {
        public async Task<Feature> AddFeature(Feature input, [Service] IFeatureRepository repo)
        {
            await repo.AddAsync(input);
            //await repo.SaveChangesAsync();
            return input;
        }
        // Add more mutations as needed
    }
}
