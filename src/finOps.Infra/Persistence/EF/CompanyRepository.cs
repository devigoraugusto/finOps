using finOps.Application.Interfaces.Repositories;
using finOps.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace finOps.Infra.Persistence.EF
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Company?> GetByGuidAsync(Guid companyGuid)
        {
            return await _context.Companies
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Guid == companyGuid);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies
                .Include(c => c.Invoices)
                .ToListAsync();
        }

        public async Task AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid companyGuid)
        {
            _context.Companies.Remove(new Company { Guid = companyGuid });
            return _context.SaveChangesAsync();
        }
    }
}
