using FluentValidation;
using finOps.Core.Entities;

public class InvoiceValidator : AbstractValidator<Invoice>
{
    public InvoiceValidator()
    {
        RuleFor(invoice => invoice.InvoiceNumber)
            .NotEmpty().WithMessage("Invoice number is required.")
            .Matches(@"^INV-\d{4}-\d{3}$").WithMessage("Invoice number must be in the format INV-YYYY-NNN, where YYYY is the year and NNN is a sequence number.");

        RuleFor(invoice => invoice.CompanyGuid)
            .NotEmpty().WithMessage("Company ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Company ID cannot be empty");

        RuleFor(invoice => invoice.Amount)
            .NotEmpty().WithMessage("Invoice amount is required.")
            .GreaterThan(0).WithMessage("Invoice amount must be positive.");

        RuleFor(invoice => invoice.DueDate)
            .NotEmpty().WithMessage("Due date is required.")
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");

    }
}