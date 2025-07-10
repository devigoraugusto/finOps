namespace finOps.Application.DTOs
{
    public class AnticipationResultDto
    {
        public string Number { get; set; } = null!;
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}