using PropertyTenants.Domain.Entities.Features;
using PropertyTenants.Gateways.GraphQL.Types.InputTypes;
using PropertyTenants.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PropertyTenants.Gateways.GraphQL.Types.Mutations
{
    public class Mutation
    {
        /// <summary>
        /// Adds a new feature
        /// </summary>
        public async Task<Feature> AddFeature(AddFeatureInput input, [Service] PropertyTenantsDbContext context)
        {
            var feature = new Feature
            {
                Name = input.Name,
                Description = input.Description,
                FeatureGroupId = input.FeatureGroupId
            };

            await context.Features.AddAsync(feature);
            await context.SaveChangesAsync();
            
            // Load the feature group for the response
            await context.Entry(feature)
                .Reference(f => f.FeatureGroup)
                .LoadAsync();
                
            return feature;
        }

        /// <summary>
        /// Updates an existing feature
        /// </summary>
        public async Task<Feature?> UpdateFeature(UpdateFeatureInput input, [Service] PropertyTenantsDbContext context)
        {
            var feature = await context.Features
                .Include(f => f.FeatureGroup)
                .FirstOrDefaultAsync(f => f.Id == input.Id);
                
            if (feature == null)
                return null;

            feature.Name = input.Name;
            feature.Description = input.Description;
            feature.FeatureGroupId = input.FeatureGroupId;

            context.Features.Update(feature);
            await context.SaveChangesAsync();
            return feature;
        }

        /// <summary>
        /// Deletes a feature by ID
        /// </summary>
        public async Task<bool> DeleteFeature(int id, [Service] PropertyTenantsDbContext context)
        {
            var feature = await context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null)
                return false;

            context.Features.Remove(feature);
            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Adds a new feature group
        /// </summary>
        public async Task<FeatureGroup> AddFeatureGroup(AddFeatureGroupInput input, [Service] PropertyTenantsDbContext context)
        {
            var featureGroup = new FeatureGroup
            {
                Name = input.Name,
                Description = input.Description
            };

            await context.FeatureGroups.AddAsync(featureGroup);
            await context.SaveChangesAsync();
            
            return featureGroup;
        }

        /// <summary>
        /// Updates an existing feature group
        /// </summary>
        public async Task<FeatureGroup?> UpdateFeatureGroup(UpdateFeatureGroupInput input, [Service] PropertyTenantsDbContext context)
        {
            var featureGroup = await context.FeatureGroups
                .Include(fg => fg.Features)
                .FirstOrDefaultAsync(fg => fg.Id == input.Id);
                
            if (featureGroup == null)
                return null;

            featureGroup.Name = input.Name;
            featureGroup.Description = input.Description;

            context.FeatureGroups.Update(featureGroup);
            await context.SaveChangesAsync();
            return featureGroup;
        }

        /// <summary>
        /// Deletes a feature group by ID
        /// </summary>
        public async Task<bool> DeleteFeatureGroup(int id, [Service] PropertyTenantsDbContext context)
        {
            var featureGroup = await context.FeatureGroups.FirstOrDefaultAsync(fg => fg.Id == id);
            if (featureGroup == null)
                return false;

            context.FeatureGroups.Remove(featureGroup);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
