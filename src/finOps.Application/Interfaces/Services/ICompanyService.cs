using finOps.Core.Entities;
using finOps.Application.DTOs;

namespace finOps.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<Company> GetByGuidAsync(Guid companyGuid);
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<Company> CreateAsync(CompanyDto companyDto);
        Task UpdateAsync(Company company);
        Task DeleteAsync(Guid companyGuid);
        decimal CalculateLimit(Company company);
    }
}