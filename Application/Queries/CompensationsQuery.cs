using Application.Queries.Contracts;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Core;

namespace Application.Queries;

public class CompensationsQuery : ICompensationsQuery
{
    private readonly CompensationsDbContext _context;

    public CompensationsQuery(CompensationsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Compensation>> GetCompensationsAsync(int year, int month, long tenantId)
    {
        return await _context
            .Compensations
            .Where(x => x.DateCompensation.InUtc().Month == month && x.DateCompensation.InUtc().Year == year && x.TenantId == tenantId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Compensation>> GetPersonalCompensationsAsync(long employeeId)
    {
        return await _context
            .Compensations
            //.Where(x => x.TenantId = 1)
            .Where(x => x.EmployeeId == employeeId)
            .AsNoTracking()
            .ToListAsync();
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