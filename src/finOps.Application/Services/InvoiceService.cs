using finOps.Application.Interfaces.Services;
using finOps.Application.Services;

namespace finOps.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICartService _cartService;
        private readonly ICompanyService _companyService;

        public InvoiceService(ICartService cartService, ICompanyService companyService)
        {
            _cartService = cartService;
            _companyService = companyService;
        }

        public async Task<InvoiceDto> GetInvoiceAsync(Guid companyGuid, Guid invoiceGuid)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");
            var invoice = company.Invoices.FirstOrDefault(i => i.GuidId == invoiceGuid);
            if (invoice == null) throw new Exception("Invoice not found in the specified company.");

            return new InvoiceDto
            {
                GuidId = invoice.GuidId,
                CompanyGuid = companyGuid,
                Amount = invoice.Amount,
                Date = invoice.Date
            };
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(Guid companyGuid, InvoiceDto invoiceDto)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");

            var newInvoice = new Invoice
            {
                GuidId = invoiceDto.GuidId,
                CompanyGuid = companyGuid,
                Amount = invoiceDto.Amount,
                Date = invoiceDto.Date
            };

            company.Invoices.Add(newInvoice);
            await _companyService.UpdateCompanyAsync(company);

            return invoiceDto;
        }

        public async Task<InvoiceDto> UpdateInvoiceAsync(Guid companyGuid, InvoiceDto invoiceDto)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");
            var invoice = company.Invoices.FirstOrDefault(i => i.GuidId == invoiceDto.GuidId);
            if (invoice == null) throw new Exception("Invoice not found in the specified company.");

            invoice.Amount = invoiceDto.Amount;
            invoice.Date = invoiceDto.Date;

            await _companyService.UpdateCompanyAsync(company);

            return invoiceDto;
        }

        public async Task<bool> DeleteInvoiceAsync(Guid companyGuid, Guid invoiceGuid)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");
            var invoice = company.Invoices.FirstOrDefault(i => i.GuidId == invoiceGuid);
            if (invoice == null) throw new Exception("Invoice not found in the specified company.");

            company.Invoices.Remove(invoice);
            await _companyService.UpdateCompanyAsync(company);

            return true;
        }

        public async Task ValidateInvoiceAsync(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice), "Invoice cannot be null.");
            if (invoice.Amount <= 0) throw new ArgumentException("Invoice amount must be greater than zero.", nameof(invoice.Amount));
            if (invoice.Date == default) throw new ArgumentException("Invoice date is not valid.", nameof(invoice.Date));
        }
    }
}