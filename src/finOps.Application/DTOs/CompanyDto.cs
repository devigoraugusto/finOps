namespace finOps.Application.DTOs
{
    public class CompanyDto
    {
        public Guid Guid { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal MonthlyBilling { get; set; }
        public string BusinessType { get; set; } = null!; // "Serviços" ou "Produtos"
        public decimal Limit { get; set; } // Limite de crédito da empresa
    }
}