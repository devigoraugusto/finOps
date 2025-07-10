using finOps.Core.Entities;

public class AnticipationService
{
    private const decimal Taxa = 0.0465m;

    public (List<AnticipationResult> notas, decimal totalBruto, decimal totalLiquido) 
        CalculateAnticipation(List<Invoice> invoices)
    {
        var results = new List<AnticipationResult>();
        decimal totalBruto = 0, totalLiquido = 0;

        foreach (var nf in invoices)
        {
            var prazo = (nf.DueDate - DateTime.Today).Days;
            if (prazo < 0) prazo = 0;
            var desagio = nf.Amount / (decimal)Math.Pow((double)(1 + Taxa), prazo / 30.0);
            var valorLiquido = nf.Amount - desagio;

            results.Add(new AnticipationResult
            {
                Numero = nf.Id.ToString(),
                ValorBruto = nf.Amount,
                ValorLiquido = Math.Round(valorLiquido, 2)
            });

            totalBruto += nf.Amount;
            totalLiquido += Math.Round(valorLiquido, 2);
        }

        return (results, totalBruto, totalLiquido);
    }
}

public class AnticipationResult
{
    public string Numero { get; set; } = null!;
    public decimal ValorBruto { get; set; }
    public decimal ValorLiquido { get; set; }
}