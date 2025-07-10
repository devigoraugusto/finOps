using Microsoft.AspNetCore.Mvc;
using finOps.Core.Entities;
using finOps.Application.Services;

[ApiController]
[Route("api/companies")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;
    private readonly AnticipationService _anticipationService;

    private static List<Company> _companies = new();
    private static Dictionary<Guid, List<Invoice>> _carts = new();

    public CompanyController(CompanyService companyService, AnticipationService anticipationService)
    {
        _companyService = companyService;
        _anticipationService = anticipationService;
    }

    [HttpPost]
    public IActionResult Register([FromBody] Company company)
    {
        company.Guid = Guid.NewGuid();
        _companies.Add(company);
        return CreatedAtAction(nameof(Get), new { id = company.Id }, company);
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var company = _companies.FirstOrDefault(x => x.Guid == guid);
        if (company == null) return NotFound();
        return Ok(company);
    }

    [HttpPost("{companyGuid}/invoices")]
    public IActionResult AddInvoice(Guid companyGuid, [FromBody] Invoice invoice)
    {
        var company = _companies.FirstOrDefault(x => x.Guid == companyGuid);
        if (company == null) return NotFound();

        invoice.Guid = Guid.NewGuid();
        company.Invoices.Add(invoice);
        return Ok(invoice);
    }

    [HttpPost("{companyGuid}/cart/add")]
    public IActionResult AddToCart(Guid companyGuid, [FromBody] Guid invoiceId)
    {
        var company = _companies.FirstOrDefault(x => x.Guid == companyGuid);
        if (company == null) return NotFound();

        var invoice = company.Invoices.FirstOrDefault(x => x.Guid == invoiceId);
        if (invoice == null) return NotFound();

        if (!_carts.ContainsKey(companyGuid))
            _carts[companyGuid] = new List<Invoice>();

        var cart = _carts[companyGuid];
        var limit = _companyService.CalculateLimit(company);
        var totalBruto = cart.Sum(x => x.Amount) + invoice.Amount;
        if (totalBruto > limit)
            return BadRequest("Limite de crédito excedido.");

        cart.Add(invoice);
        return Ok(cart);
    }

    [HttpPut("{companyGuid}/cart/remove")]
    public IActionResult RemoveFromCart(Guid companyGuid, [FromBody] Guid invoiceId)
    {
        if (!_carts.ContainsKey(companyGuid)) return NotFound();
        var cart = _carts[companyGuid];
        var invoice = cart.FirstOrDefault(x => x.Guid == invoiceId);
        if (invoice == null) return NotFound();

        cart.Remove(invoice);
        return Ok(cart);
    }

    [HttpGet("{companyGuid}/cart/checkout")]
    public IActionResult Checkout(Guid companyGuid)
    {
        var company = _companies.FirstOrDefault(x => x.Guid == companyGuid);
        if (company == null) return NotFound();
        if (!_carts.ContainsKey(companyGuid)) return BadRequest("Cart empty.");

        var cart = _carts[companyGuid];
        var limit = _companyService.CalculateLimit(company);
        var (notas, totalBruto, totalLiquido) = _anticipationService.CalculateAnticipation(cart);

        return Ok(new
        {
            empresa = company.Name,
            cnpj = company.DocumentNumber,
            limite = limit,
            notas_fiscais = notas,
            total_liquido = totalLiquido,
            total_bruto = totalBruto
        });
    }
}