using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Domain.Repositories;

namespace PropertyTenants.Gateways.GraphQL.Types.Queries
{
    public class Query
    {
        public Task<IEnumerable<Feature>> GetFeatures([Service] IFeatureRepository repo) => repo.GetAllAsync();
        public Task<Feature?> GetFeatureById(int id, [Service] IFeatureRepository repo) => repo.GetByIdAsync(id);
        public Task<IEnumerable<FeatureGroup>> GetFeatureGroups([Service] IFeatureGroupRepository repo) => repo.GetAllAsync();
    }
}
