using finOps.Application.Interfaces.Services;
using finOps.Application.DTOs;
using finOps.Core.Entities;

namespace finOps.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GetInvoiceAsync(Guid companyGuid, Guid invoiceGuid);
        Task<Invoice> CreateInvoiceAsync(Guid companyGuid, InvoiceDto invoiceDto);
        Task<Invoice> UpdateInvoiceAsync(Guid companyGuid, Invoice invoice);
        Task<bool> DeleteInvoiceAsync(Guid companyGuid, Guid invoiceGuid);
        void ValidateInvoice(Invoice invoice);
    }
}