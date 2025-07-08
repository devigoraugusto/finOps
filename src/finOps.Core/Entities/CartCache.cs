namespace finOps.Core.Entities
{
    public class CartCache
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public Guid CompanyId { get; set; }
    }

}