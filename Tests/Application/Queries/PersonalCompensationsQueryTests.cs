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
                    TenantId = 600
                }
            });

            var queryResult = await query.GetPersonalCompensationsAsync(200, 600);

            Assert.Single(queryResult);
            Assert.Equal(100500, queryResult.Single().Id);
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
