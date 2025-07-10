using finOps.Core.Cache;

namespace finOps.Application.Interfaces.Repositories
{
    public interface ICartCacheRepository
    {
        Task<CartCache?> GetCartCacheAsync(Guid companyGuid);
        Task SaveCartCache(CartCache cartCache);
        Task DeleteCartCache(Guid companyGuid);
    }
}
