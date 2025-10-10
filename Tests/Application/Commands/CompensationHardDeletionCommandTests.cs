using Application.Commands;
using Core;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Application.Commands
{
  public class CompensationHardDeletionCommandTests
  {
    private readonly CompensationsDbContext _context;
    private readonly CompensationHardDeletionCommand _command;

    public CompensationHardDeletionCommandTests()
    {
      var options = new DbContextOptionsBuilder<CompensationsDbContext>()
        .UseInMemoryDatabase(databaseName: "HardDeleteCompensationCommandDatabase")
        .Options;

      _context = new CompensationsDbContext(options);
      _command = new CompensationHardDeletionCommand(_context);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCompensationFromDbSet()
    {
      var compensation = new Compensation(
        1,
        "Test",
        100.0,
        new Employee(),
        DateTime.Now.ToString(),
        1,
        false
      );

      _context
        .Compensations
        .Add(compensation);
      await _context.SaveChangesAsync();

      await _command.ExecuteAsync(compensation.Id);

      var deletedCompensation = await _context
        .Compensations
        .FindAsync(compensation.Id);

      Assert.Null(deletedCompensation);
    }
  }
}
