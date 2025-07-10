using finOps.Application.Interfaces.Services;
using finOps.Application.DTOs;

namespace finOps.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartService _cartService;
        private readonly ICompanyService _companyService;
        private readonly IInvoiceService _invoiceService;

        public CartService(ICartService cartService, ICompanyService companyService, IInvoiceService invoiceService)
        {
            _cartService = cartService;
            _companyService = companyService;
            _invoiceService = invoiceService;
        }

        public async Task<CartDto> GetCartAsync(Guid companyGuid)
        {
            return await _cartService.GetCartAsync(companyGuid);
        }
        
        public async Task<CartDto> CreateCartAsync(Guid companyGuid, CartDto cartDto)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");
            var invoice = company.Invoices.FirstOrDefault(i => i.GuidId == cartDto.InvoiceId);
            if (invoice == null) throw new Exception("Invoice not found in the specified company.");

            return await _cartService.CreateCartAsync(companyGuid, cartDto);
        }

        public async Task<CartDto> UpdateCartAsync(Guid companyGuid, CartDto cartDto)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");
            var invoice = company.Invoices.FirstOrDefault(i => i.GuidId == cartDto.InvoiceId);
            if (invoice == null) throw new Exception("Invoice not found in the specified company.");

            return await _cartService.UpdateCartAsync(companyGuid, cartDto);
        }

        public async Task<bool> DeleteCartAsync(Guid companyGuid)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");

            return await _cartService.DeleteCartAsync(companyGuid);
        }

    }
}