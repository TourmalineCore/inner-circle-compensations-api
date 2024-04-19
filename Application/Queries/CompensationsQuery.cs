using Application.Queries.Contracts;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Core;

namespace Application.Queries;

public class CompensationsQuery : ICompensationsQuery
{
    private readonly CompensationsDbContext _context;

    private readonly ITenantCompensationsQuery _tenantCompensationsQuery;

    public CompensationsQuery(CompensationsDbContext context, ITenantCompensationsQuery tenantCompensationsQuery)
    {
        _context = context;
        _tenantCompensationsQuery = tenantCompensationsQuery;
    }

    public async Task<List<Compensation>> GetCompensationsAsync(int year, int month, long tenantId)
    {
        var compensations = await _tenantCompensationsQuery.GetCompensationsByTenantIdAsync(tenantId);

        return compensations
            .Where(x => x.DateCompensation.InUtc().Month == month && x.DateCompensation.InUtc().Year == year)
            .ToList();
    }

    public async Task<List<Compensation>> GetCompensationsByIdsAsync(long[] ids)
    {
        var compensations = await _context
            .Compensations
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        if (compensations.Count == 0)
        {
            throw new ArgumentException("All compensations not found");
        }

        if (compensations.Count != ids.Length)
        {
            throw new ArgumentException($"Couldn't find all compensations. Found items for ids: {string.Join(", ", compensations.Select(x => x.Id))}");
        }

        return compensations;
    }
}