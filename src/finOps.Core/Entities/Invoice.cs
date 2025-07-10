using finOps.Core.Enums;

namespace finOps.Core.Entities
{
    public class Invoice
    {
        public int Id { get; set; } = 0; // Default value for Id
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string InvoiceNumber { get; set; } = default!; // e.g., "INV-2023-001"
        public Guid CompanyGuid { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; } = default!;
        public DateTime DueDate { get; set; } = default!;
        public InvoiceStatusEnum Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public Company Company { get; set; } = default!;
    }
}