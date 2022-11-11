using Walks.API.Models.Domain;

namespace Walks.API.Repositories
{
    public interface IWalkRepository
    {

        Task<IEnumerable<Walk>> GetAllAsync();
    }
}
