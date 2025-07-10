namespace finOps.Application.DTOs
{
    public class CheckoutResponseDto
    {
        public string Empresa { get; set; } = null!;
        public string Cnpj { get; set; } = null!;
        public decimal Limite { get; set; }
        public List<AnticipationResultDto> NotasFiscais { get; set; } = new();
        public decimal TotalLiquido { get; set; }
        public decimal TotalBruto { get; set; }
    }
}