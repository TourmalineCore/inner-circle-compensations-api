using Application.Queries;
using Core;
using DataAccess;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Tests.Application.Queries
{
    public class PersonalCompensationsQueryTests
    {
        [Fact]
        public async Task UnknownEmployeeIdAndTenantId_ShouldFindNoData()
        {
            var query = BuildQuery(new List<Compensation>());

            Assert.Empty(await query.GetPersonalCompensationsAsync(100, 500));
        }

        [Fact]
        public async Task SingleRecordAvailable_ShouldFindItByEmployeeIdAndTenantId()
        {
            var query = BuildQuery(new List<Compensation> {
                new Compensation
                {
                    Id = 100500,
                    EmployeeId = 200,
                    TenantId = 600,
                    Quantity = 1
                }
            });

            var queryResult = await query.GetPersonalCompensationsAsync(200, 600);

            Assert.Single(queryResult);
            Assert.Equal(100500, queryResult.Single().Id);
            Assert.Equal(1, queryResult.Single().Quantity);
        }

        [Fact]
        public async Task CompensationsWithDifferentTenantsForTheSameEmployee_ShouldFindCompensationsOnlyForTheNeededTenant()
        {
            var query = BuildQuery(new List<Compensation> {
                new Compensation
                {
                    Id = 1,
                    EmployeeId = 777,
                    TenantId = 12345,
                    Quantity = 1
                },
                new Compensation
                {
                    Id = 2,
                    EmployeeId = 777,
                    TenantId = 69,
                    Quantity = 1
                },
                new Compensation
                {
                    Id = 3,
                    EmployeeId = 777,
                    TenantId = 12345,
                    Quantity = 1
                },
            });

            var queryResult = await query.GetPersonalCompensationsAsync(777, 69);

            Assert.Single(queryResult);
            Assert.Equal(2, queryResult.Single().Id);
            Assert.Equal(1, queryResult.Single().Quantity);
        }

        private static PersonalCompensationsQuery BuildQuery(List<Compensation> compensations)
        {
            var dbContextMock = new Mock<CompensationsDbContext>();

            dbContextMock
                .Setup(x => x.Compensations)
                .ReturnsDbSet(compensations);

            return new PersonalCompensationsQuery(dbContextMock.Object);
        }
    }
}