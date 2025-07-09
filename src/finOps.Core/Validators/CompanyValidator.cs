using FluentValidation;
using finOps.Core.Entities;

public class CompanyValidator : AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(company => company.Name)
            .NotEmpty().WithMessage("Company name is required.")
            .Length(2, 100).WithMessage("Company name must be between 2 and 100 characters.");

        RuleFor(company => company.DocumentNumber)
            .NotEmpty().WithMessage("CNPJ number is required.")
            .Length(14).WithMessage("CNPJ number must be 14 characters long.");

        RuleFor(company => company.MonthlyBilling)
            .NotEmpty().WithMessage("Monthly billing is required.")
            .GreaterThan(0).WithMessage("Monthly billing must be positive.");

        RuleFor(company => company.Industry)
            .IsInEnum().WithMessage("Industry must be a valid enum value.");
    }
}