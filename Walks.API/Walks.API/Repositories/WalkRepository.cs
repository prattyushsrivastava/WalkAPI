using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;

namespace Walks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalkDbContext walkDb;

        public WalkRepository(WalkDbContext walkDb)
        {
            this.walkDb = walkDb;
        }
        
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await walkDb.Walks.ToListAsync();
            
        }
    }
}
