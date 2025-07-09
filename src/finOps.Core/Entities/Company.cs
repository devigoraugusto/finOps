using finOps.Core.Enums;

namespace finOps.Core.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; } = default!;
        public string DocumentNumber { get; set; } = default!;
        public decimal MonthlyBilling { get; set; } = default!;
        public EnumIndustry Industry { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
