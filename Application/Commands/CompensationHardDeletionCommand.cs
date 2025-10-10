using DataAccess;

namespace Application.Commands;

public class CompensationHardDeletionCommand
{
  private readonly CompensationsDbContext _context;

  public CompensationHardDeletionCommand(CompensationsDbContext сompensationsDbContext)
  {
    _context = сompensationsDbContext;
  }

  public async Task ExecuteAsync(long id)
  {
    var compensation = _context
      .Compensations
      .SingleOrDefault(x => x.Id == id);

    if (compensation != null)
    {
      _context.Remove(compensation);
      await _context.SaveChangesAsync();
    }
  }
}
