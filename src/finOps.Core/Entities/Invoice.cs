using finOps.Core.Enums;

namespace finOps.Core.Entities
{
    public class Invoice : Entity
    {
        public Guid CompanyGuid { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public EnumInvoiceStatus Status { get; set; } // e.g., Paid, Unpaid, Overdue

        // Navigation property
        public Company Company { get; set; }
    }
}