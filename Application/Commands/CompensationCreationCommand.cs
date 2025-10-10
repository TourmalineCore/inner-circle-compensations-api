using Application.Dtos;
using Core;
using DataAccess;

namespace Application.Commands;

public class CompensationCreationCommand
{
  private readonly CompensationsDbContext _context;

  public CompensationCreationCommand(CompensationsDbContext сompensationsDbContext)
  {
    _context = сompensationsDbContext;
  }

  public async Task<List<long>> ExecuteAsync(CompensationCreateDto dto, Employee employee)
  {
    var compensations = dto
      .Compensations
      .Select(x => new Compensation
      (
        x.TypeId,
        x.Comment,
        x.Amount,
        employee,
        dto.CompensationRequestedForYearAndMonth,
        x.Quantity,
        x.IsPaid
      ))
      .ToList();

    await _context.AddRangeAsync(compensations);
    await _context.SaveChangesAsync();

    return compensations
      .Select(compensation => compensation.Id)
      .ToList();
  }
}
