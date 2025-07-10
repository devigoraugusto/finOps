using finOps.Application.Interfaces;
using finOps.Core.Cache;
using Microsoft.AspNetCore.Mvc;

namespace finOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICompanyService _companyService;
    private readonly IInvoiceService _invoiceService;

    public CartController(ICartService cartService, ICompanyService companyService, IInvoiceService invoiceService)
    {
        _cartService = cartService;
        _companyService = companyService;
        _invoiceService = invoiceService;
    }

    [HttpGet("{companyGuid}")]
    public async Task<IActionResult> GetCart(Guid companyGuid)
    {
        var cart = await _cartService.GetCartAsync(companyGuid);
        if (cart == null)
            return NotFound("Cart not found for the specified company.");

        return Ok(cart);
    }

    [HttpPost("{companyGuid}")]
    public async Task<IActionResult> AddToCart(Guid companyGuid, [FromBody] Guid InvoiceGuid)
    {
        var company = await _companyService.GetCompanyByIdAsync(companyGuid);
        if (company == null) return NotFound("Company not found.");

        var invoice = company.Invoices.FirstOrDefault(i => i.GuidId == InvoiceGuid);
        if (invoice == null) return NotFound("Invoice not found in the specified company.");

        try {
            _invoiceService.ValidateInvoice(invoice);
        }
        catch (Exception ex) {
            return BadRequest($"Invalid invoice data: {ex.Message}");
        }

        var cart = await CartCache { CompanyGuid = companyGuid };
        if (cart == null)
            cart = await _cartService.GetCartAsync(companyGuid);

        if (cart == null)
            return BadRequest("Invalid cart data.");

        var createdCart = await _cartService.CreateCartAsync(companyGuid, cartDto);
        return CreatedAtAction(nameof(GetCart), new { companyGuid = createdCart.CompanyGuid }, createdCart);
    }

}
