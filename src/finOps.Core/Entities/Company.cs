using finOps.Core.Enums;

namespace finOps.Core.Entities
{
    public class Company
    {
        public int Id { get; set; } = 0; // Default value for Id
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public string DocumentNumber { get; set; } = default!;
        public decimal MonthlyBilling { get; set; } = default!;
        public BusinessTypeEnum BusinessType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
