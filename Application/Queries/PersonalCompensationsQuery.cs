using Application.Queries.Contracts;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Core;

namespace Application.Queries;

public class PersonalCompensationsQuery : IPersonalCompensationsQuery
{
    private readonly CompensationsDbContext _context;

    public PersonalCompensationsQuery(CompensationsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Compensation>> GetPersonalCompensationsAsync(long employeeId, long tenantId)
    {
        return await _context
            .Compensations
            .Where(x => x.TenantId == tenantId)
            .Where(x => x.EmployeeId == employeeId)
            .AsNoTracking()
            .ToListAsync();
    }
}