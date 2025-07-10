using finOps.Application.Interfaces.Services;
using finOps.Core.Entities;
using finOps.Application.DTOs;
using finOps.Core.Enums;

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
            var invoice = company.Invoices.FirstOrDefault(i => i.Guid == invoiceGuid);
            if (invoice == null) throw new Exception("Invoice not found in the specified company.");

            return new InvoiceDto
            {
                Number = invoice.InvoiceNumber,
                Amount = invoice.Amount,
                DueDate = invoice.DueDate
            };
        }

        public async Task<Invoice> CreateInvoiceAsync(Guid companyGuid, InvoiceDto invoiceDto)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");

            var newInvoice = new Invoice
            {
                Guid = Guid.NewGuid(),
                CompanyGuid = companyGuid,
                InvoiceNumber = invoiceDto.Number,
                Amount = invoiceDto.Amount,
                DueDate = invoiceDto.DueDate,
                IssueDate = DateTime.UtcNow,
                Status = InvoiceStatusEnum.Unpaid,
            };

            company.Invoices.Add(newInvoice);
            await _companyService.UpdateAsync(company);

            return newInvoice;
        }

        public async Task<Invoice> UpdateInvoiceAsync(Guid companyGuid, Invoice invoice)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");
            var beforeInvoice = company.Invoices.FirstOrDefault(i => i.Guid == invoice.Guid);
            if (beforeInvoice == null) throw new Exception("Invoice not found in the specified company.");

            beforeInvoice.Amount = invoice.Amount;
            beforeInvoice.DueDate = invoice.DueDate;

            await _companyService.UpdateAsync(company);

            return invoice;
        }

        public async Task<bool> DeleteInvoiceAsync(Guid companyGuid, Guid invoiceGuid)
        {
            var company = await _companyService.GetByGuidAsync(companyGuid);
            if (company == null) throw new Exception("Company not found.");
            var invoice = company.Invoices.FirstOrDefault(i => i.Guid == invoiceGuid);
            if (invoice == null) throw new Exception("Invoice not found in the specified company.");

            company.Invoices.Remove(invoice);
            await _companyService.UpdateAsync(company);

            return true;
        }

        public void ValidateInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice), "Invoice cannot be null.");
            if (invoice.Amount <= 0) throw new ArgumentException("Invoice amount must be greater than zero.", nameof(invoice.Amount));
            if (invoice.DueDate == default) throw new ArgumentException("Invoice date is not valid.", nameof(invoice.DueDate));
            if (invoice.IssueDate > DateTime.UtcNow)
            {
                throw new ArgumentException("Invoice issue date cannot be in the future.", nameof(invoice.IssueDate));
            }
            if (invoice.DueDate < invoice.IssueDate)
            {
                throw new ArgumentException("Invoice due date cannot be earlier than the issue date.", nameof(invoice.DueDate));
            }
        }
    }
}