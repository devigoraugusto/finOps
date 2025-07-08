using finOps.Core.Enums;

namespace finOps.Core.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public string MonthlyBilling { get; set; }
        public EnumIndustry Industry { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
