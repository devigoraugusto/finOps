using finOps.Core.Entities;
using finOps.Core.Enums;
using finOps.Application.DTOs;
using finOps.Application.Interfaces.Services;
using finOps.Application.Interfaces.Repositories;

public class CompanyService : ICompanyService
{
    private const decimal LimitInitial = 10000m;
    private const decimal LimitMedium = 50000m;
    private const decimal LimitHigh = 100000m;

    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company> GetByGuidAsync(Guid companyGuid)
    {
        var company = await _companyRepository.GetByGuidAsync(companyGuid) ?? throw new Exception("Company not found.");

        return new Company
        {
            Guid = company.Guid,
            DocumentNumber = company.DocumentNumber,
            Name = company.Name,
            MonthlyBilling = company.MonthlyBilling,
            BusinessType = company.BusinessType,
        };
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(company => new CompanyDto
        {
            Guid = company.Guid,
            DocumentNumber = company.DocumentNumber,
            Name = company.Name,
            MonthlyBilling = company.MonthlyBilling,
            BusinessType = company.BusinessType.ToString(),
            Limit = CalculateLimit(company)
        });
    }

    public async Task<Company> CreateAsync(CompanyDto companyDto)
    {
        var company = new Company
        {
            Guid = Guid.NewGuid(),
            DocumentNumber = companyDto.DocumentNumber,
            Name = companyDto.Name,
            MonthlyBilling = companyDto.MonthlyBilling,
            BusinessType = Enum.Parse<BusinessTypeEnum>(companyDto.BusinessType)
        };

        await _companyRepository.AddAsync(company);

        return company;
    }

    public async Task UpdateAsync(Company companyDto)
    {
        var company = new Company
        {
            Guid = companyDto.Guid,
            DocumentNumber = companyDto.DocumentNumber,
            Name = companyDto.Name,
            MonthlyBilling = companyDto.MonthlyBilling,
            BusinessType = companyDto.BusinessType
        };

        await _companyRepository.UpdateAsync(company);
    }

    public async Task DeleteAsync(Guid companyGuid)
    {
        await _companyRepository.DeleteAsync(companyGuid);
    }

    public decimal CalculateLimit(Company company)
    {
        var bill = company.MonthlyBilling;
        var BusinessType = company.BusinessType;

        if (bill >= LimitInitial && bill <= LimitMedium)
            return bill * 0.5m;
        
        if (bill > LimitMedium && bill <= LimitHigh)
        {
            return BusinessType switch
            {
                BusinessTypeEnum.Services => bill * 0.55m,
                BusinessTypeEnum.Products => bill * 0.6m,
                _ => 0
            };
        }
        
        if (bill > LimitHigh)
        {
            return BusinessType switch
            {
                BusinessTypeEnum.Services => bill * 0.6m,
                BusinessTypeEnum.Products => bill * 0.65m,
                _ => 0
            };
        }

        return 0;
    }
}