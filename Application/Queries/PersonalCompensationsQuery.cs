using Application.Queries.Contracts;
using Core;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class PersonalCompensationsQuery : IPersonalCompensationsQuery
{
    private readonly CompensationsDbContext _context;

    public PersonalCompensationsQuery(CompensationsDbContext context)
    {
        _context = context;
    }

    public Task<List<Compensation>> GetPersonalCompensationsAsync(long employeeId, long tenantId)
    {
        return _context
            .Compensations
            .AsNoTracking()
            .Where(x => x.TenantId == tenantId)
            .Where(x => x.EmployeeId == employeeId)
            .ToListAsync();
    }
}
