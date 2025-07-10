using finOps.Application.Interfaces.Services;

namespace finOps.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GetInvoiceAsync(Guid companyGuid, Guid invoiceGuid);
        Task<InvoiceDto> CreateInvoiceAsync(Guid companyGuid, InvoiceDto invoiceDto);
        Task<InvoiceDto> UpdateInvoiceAsync(Guid companyGuid, InvoiceDto invoiceDto);
        Task<bool> DeleteInvoiceAsync(Guid companyGuid, Guid invoiceGuid);

        Task ValidateInvoiceAsync(Invoice invoice);
    }
}