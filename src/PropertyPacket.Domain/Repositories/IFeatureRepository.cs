using PropertyTenants.Domain.Entities.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyTenants.Domain.Repositories
{
    public interface IFeatureRepository : IBaseRepository<Feature>
    {
        Task<Feature?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Feature>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Feature feature, CancellationToken cancellationToken = default);
        void Update(Feature feature);
        void Remove(Feature feature);
    }

    public interface IFeatureGroupRepository : IBaseRepository<FeatureGroup>
    {
        Task<FeatureGroup?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<FeatureGroup>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(FeatureGroup group, CancellationToken cancellationToken = default);
        void Update(FeatureGroup group);
        void Remove(FeatureGroup group);
    }
}
