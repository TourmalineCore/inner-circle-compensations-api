using Application.Dtos;
using DataAccess;
using Core;

namespace Application.Commands;

public class CompensationCreationCommand
{
    private readonly CompensationsDbContext _context;

    public CompensationCreationCommand(CompensationsDbContext CompensationsDbContext)
    {
        _context = CompensationsDbContext;
    }

    public async Task ExecuteAsync(CompensationCreateDto dto, Employee employee)
    {
        var compensations = dto.Compensations.Select(x => new Compensation(x.TypeId, x.Comment, x.Amount, employee, dto.DateCompensation, x.IsPaid));

        await _context.AddRangeAsync(compensations);
        await _context.SaveChangesAsync();
    }
}
