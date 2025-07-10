using finOps.Core.Entities;

namespace finOps.Core.Cache
{
    public class CartCache
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public List<Invoice> Invoices { get; set; } = [];
        public Guid CompanyGuid { get; set; }
    }

}