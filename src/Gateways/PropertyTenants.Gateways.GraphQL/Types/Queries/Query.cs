using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types.Queries
{
    public class Query
    {
        /// <summary>
        /// Gets all features
        /// </summary>
        public async Task<IEnumerable<Feature>> GetFeatures([Service] PropertyTenantsDbContext context)
            => await context.Features.Include(f => f.FeatureGroup).ToListAsync();

        /// <summary>
        /// Gets a specific feature by ID
        /// </summary>
        public async Task<Feature?> GetFeatureById(int id, [Service] PropertyTenantsDbContext context)
            => await context.Features.Include(f => f.FeatureGroup).FirstOrDefaultAsync(f => f.Id == id);

        /// <summary>
        /// Gets all feature groups
        /// </summary>
        public async Task<IEnumerable<FeatureGroup>> GetFeatureGroups([Service] PropertyTenantsDbContext context)
            => await context.FeatureGroups.Include(fg => fg.Features).ToListAsync();

        /// <summary>
        /// Gets a specific feature group by ID
        /// </summary>
        public async Task<FeatureGroup?> GetFeatureGroupById(int id, [Service] PropertyTenantsDbContext context)
            => await context.FeatureGroups.Include(fg => fg.Features).FirstOrDefaultAsync(fg => fg.Id == id);

        /// <summary>
        /// Gets features by feature group ID
        /// </summary>
        public async Task<IEnumerable<Feature>> GetFeaturesByGroupId(int featureGroupId, [Service] PropertyTenantsDbContext context)
            => await context.Features.Where(f => f.FeatureGroupId == featureGroupId).Include(f => f.FeatureGroup).ToListAsync();
    }
}
