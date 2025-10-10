using Application.Queries.Contracts;
using Core;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class CompensationsQuery : ICompensationsQuery
{
  private readonly CompensationsDbContext _context;

  public CompensationsQuery(CompensationsDbContext context)
  {
    _context = context;
  }

  public Task<List<Compensation>> GetCompensationsAsync(int year, int month, long tenantId)
  {
    return _context
      .Compensations
      .AsNoTracking()
      .Where(x => x.TenantId == tenantId)
      //.Where(x => x.CompensationRequestedForYearAndMonth.InUtc().Month == month && x.CompensationRequestedForYearAndMonth.InUtc().Year == year)
      .Where(x => x.CompensationRequestedForYearAndMonth.Month == month && x.CompensationRequestedForYearAndMonth.Year == year)
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
