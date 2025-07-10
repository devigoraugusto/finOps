using finOps.Core.Entities;
using finOps.Application.DTOs;

namespace finOps.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetByGuidAsync(Guid companyGuid);
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto> CreateAsync(CompanyDto companyDto);
        Task UpdateAsync(CompanyDto companyDto);
        Task DeleteAsync(Guid companyGuid);
    }
}