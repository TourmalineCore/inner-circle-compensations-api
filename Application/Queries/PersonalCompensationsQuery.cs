using Application.Queries.Contracts;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Core;

namespace Application.Queries;

public class PersonalCompensationsQuery : IPersonalCompensationsQuery
{
    private readonly CompensationsDbContext _context;

    private readonly ITenantCompensationsQuery _tenantCompensationsQuery;

    public PersonalCompensationsQuery(CompensationsDbContext context, ITenantCompensationsQuery tenantCompensationsQuery)
    {
        _context = context;
        _tenantCompensationsQuery = tenantCompensationsQuery;
    }

    public async Task<List<Compensation>> GetPersonalCompensationsAsync(long employeeId, long tenantId)
    {
        var compensations = await _tenantCompensationsQuery.GetCompensationsByTenantIdAsync(tenantId);

        return compensations
            .Where(x => x.EmployeeId == employeeId)
            .ToList();
    }
}