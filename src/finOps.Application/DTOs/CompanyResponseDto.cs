namespace finOps.Application.DTOs
{
    public class CompanyResponseDto
    {
        public Guid Guid { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal MonthlyBilling { get; set; }
        public string BusinessType { get; set; } = null!;
        public decimal Limit { get; set; }
    }
}