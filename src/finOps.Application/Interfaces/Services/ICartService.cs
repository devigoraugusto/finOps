using finOps.Application.DTOs;

namespace finOps.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(Guid companyGuid);
        Task<CartDto> CreateCartAsync(Guid companyGuid, CartDto cartDto);
        Task<CartDto> UpdateCartAsync(Guid companyGuid, CartDto cartDto);
        Task<bool> DeleteCartAsync(Guid companyGuid);
    }
}