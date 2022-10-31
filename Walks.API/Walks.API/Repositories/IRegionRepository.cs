using Walks.API.Models.Domain;

namespace Walks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GettAllAsync();

        Task<Region> GetAsync(Guid id);

        Task<Region> AddAsync(Region region);
    }

}
