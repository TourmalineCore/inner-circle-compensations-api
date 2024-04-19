using Core;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Application.Queries.Contracts;

namespace Application.Queries;

public class TenantCompensationsQuery : ITenantCompensationsQuery
{
    private readonly CompensationsDbContext _context;

    public TenantCompensationsQuery(CompensationsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Compensation>> GetCompensationsByTenantIdAsync(long tenantId)
    {
        return await _context
            .Compensations
            .AsNoTracking()
            .Where(x => x.TenantId == tenantId)
            .ToListAsync();
    }
}
