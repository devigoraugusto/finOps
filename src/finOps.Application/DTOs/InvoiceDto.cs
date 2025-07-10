namespace finOps.Application.DTOs
{
    public class InvoiceDto
    {
        public string Number { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}