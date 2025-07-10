using finOps.Application.DTOs;
using finOps.Core.Cache;

namespace finOps.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartCache> GetCartAsync(Guid companyGuid);
        Task<CartCache> CreateCartAsync(Guid companyGuid);
        Task UpdateCartAsync(CartCache cart);
        Task DeleteCartAsync(Guid companyGuid);
    }
}