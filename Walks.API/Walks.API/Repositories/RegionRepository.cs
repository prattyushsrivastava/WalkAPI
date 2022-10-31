using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;

namespace Walks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalkDbContext walkDb;

        public RegionRepository(WalkDbContext walkDb)
        {
            this.walkDb = walkDb;
        }
        public async Task<Region> AddAsync(Region region)
        {
            region.Id=Guid.NewGuid();
            await walkDb.Regions.AddAsync(region);
            await walkDb.SaveChangesAsync();

            return region;

        }
        public async Task<IEnumerable<Region>> GettAllAsync()
        {
            return await walkDb.Regions.ToListAsync();
        }
        public async Task<Region> GetAsync(Guid id)
        {
           return await walkDb.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
