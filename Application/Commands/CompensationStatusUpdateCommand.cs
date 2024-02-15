using DataAccess;
using Core;

namespace Application.Commands;

public class CompensationStatusUpdateCommand
{
    private readonly CompensationsDbContext _context;

    public CompensationStatusUpdateCommand(CompensationsDbContext CompensationsDbContext)
    {
        _context = CompensationsDbContext;
    }

    public async Task ExecuteAsync(List<Compensation> compensations)
    {
        _context.UpdateRange(compensations);
        await _context.SaveChangesAsync();
    }
}