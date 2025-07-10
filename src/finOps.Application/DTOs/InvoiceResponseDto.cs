namespace finOps.Application.DTOs
{
    public class InvoiceResponseDto
    {
        public Guid Guid { get; set; }
        public string Number { get; set; } = null!;
        public decimal Value { get; set; }
        public DateTime DueDate { get; set; }
    }
}