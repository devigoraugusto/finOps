using finOps.Core.Entities;

namespace finOps.Application.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByGuidAsync(Guid companyGuid);
        Task<IEnumerable<Company>> GetAllAsync();
        Task AddAsync(Company company);
        Task UpdateAsync(Company company);
        Task DeleteAsync(Guid companyGuid);
    }
}
