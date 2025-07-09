using finOps.Core.Enums;

namespace finOps.Core.Entities
{
    public class Invoice : Entity
    {
        public string InvoiceNumber { get; set; } = default!; // e.g., "INV-2023-001"
        public Guid CompanyGuid { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; } = default!;
        public DateTime DueDate { get; set; } = default!;
        public EnumInvoiceStatus Status { get; set; } = default!;

        // Navigation property
        public Company Company { get; set; } = default!;
    }
}