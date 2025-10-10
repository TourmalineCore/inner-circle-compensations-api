using Core;
using DataAccess;

namespace Application.Commands;

public class CompensationStatusUpdateCommand
{
  private readonly CompensationsDbContext _context;

  public CompensationStatusUpdateCommand(CompensationsDbContext сompensationsDbContext)
  {
    _context = сompensationsDbContext;
  }

  public async Task ExecuteAsync(List<Compensation> compensations)
  {
    _context.UpdateRange(compensations);
    await _context.SaveChangesAsync();
  }
}
