using Application.Commands;
using Application.Dtos;
using Core;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Application.Commands
{
    public class CompensationCreationCommandTests
    {
        private readonly CompensationsDbContext _context;

        private readonly CompensationCreationCommand _command;

        public CompensationCreationCommandTests()
        {
            var options = new DbContextOptionsBuilder<CompensationsDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateCompensationCommandDatabase")
                .Options;

            _context = new CompensationsDbContext(options);
            _command = new CompensationCreationCommand(_context);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateCompensation()
        {
            var tenantId = 1L;

            var compensationCreateDto = new CompensationCreateDto()
            {
                Compensations = new List<CompensationDto>()
                {
                    new CompensationDto()
                    {
                        Amount = 100,
                        Comment = "Test",
                        IsPaid = false,
                        Quantity = 1,
                        TypeId = 1
                    }
                },
                CompensationRequestedForYearAndMonth = "2024-12-31"
            };

            var compensationIds = await _command.ExecuteAsync(
                compensationCreateDto, 
                new Employee()
                {
                    Id = 1,
                    TenantId = tenantId, 
                    CorporateEmail = "test@tourmalinecore.com",
                    FullName = "Test Test Test"
                }
                );

            var compensation = await _context.Compensations.FindAsync(compensationIds.First());
            Assert.NotNull(compensation);
            Assert.Equal(1, compensation.Quantity);
            Assert.Equal(100, compensation.Amount);
            Assert.Equal(1, compensation.TypeId);
        }
    }
}
