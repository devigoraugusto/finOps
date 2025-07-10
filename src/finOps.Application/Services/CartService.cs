using finOps.Application.Interfaces.Services;
using finOps.Application.DTOs;
using finOps.Core.Cache;
using finOps.Application.Interfaces.Repositories;

namespace finOps.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartCacheRepository _cartRepository;
        private readonly ICompanyService _companyService;
        private readonly IInvoiceService _invoiceService;

        public CartService(ICartCacheRepository cartCacheRepository, ICompanyService companyService, IInvoiceService invoiceService)
        {
            _cartRepository = cartCacheRepository;
            _companyService = companyService;
            _invoiceService = invoiceService;
        }

        public async Task<CartCache> GetCartAsync(Guid companyGuid)
        {
            return await _cartRepository.GetCartCacheAsync(companyGuid);
        }
        
        public async Task<CartCache> CreateCartAsync(Guid companyGuid)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");

            return await _cartRepository.GetCartCacheAsync(companyGuid);
        }

        public async Task UpdateCartAsync(CartCache cart)
        {
            await _cartRepository.SaveCartCache(cart);
        }

        public async Task DeleteCartAsync(Guid companyGuid)
        {
            await _cartRepository.DeleteCartCache(companyGuid);
        }

    }
}