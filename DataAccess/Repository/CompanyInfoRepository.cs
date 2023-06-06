using Microsoft.EntityFrameworkCore;

using MscApi.Domain.Entities;
using MscApi.DataAccess.Repository.Interfaces;

namespace MscApi.DataAccess.Repository;

public class CompanyInfoRepository : ICompanyInfoRepository
{
    private readonly MedStaCruzContext _context;
    public CompanyInfoRepository(MedStaCruzContext context)
    {
        _context = context;
    }

    public async Task<CompanyInformation> GetCompanyInfo()
    {
        CompanyInformation info;

        if(_context.CompanyInformation == null)
            throw new DbUpdateException();

        info = await _context.CompanyInformation.FirstOrDefaultAsync() ??
            throw new ArgumentNullException() ;

        return info;
    }
}