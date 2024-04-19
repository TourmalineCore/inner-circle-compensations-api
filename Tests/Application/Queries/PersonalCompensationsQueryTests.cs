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
            var dbContextMock = new Mock<CompensationsDbContext>();

            dbContextMock
                .Setup(x => x.Compensations)
                .ReturnsDbSet(new List<Compensation>());

            var query = new PersonalCompensationsQuery(dbContextMock.Object);

            Assert.Empty(await query.GetPersonalCompensationsAsync(100, 500));
        }

        [Fact]
        public async Task SingleRecordAvailable_ShouldFindItByEmployeeIdAndTenantId()
        {
            var dbContextMock = new Mock<CompensationsDbContext>();

            dbContextMock
                .Setup(x => x.Compensations)
                .ReturnsDbSet(new List<Compensation>
                {
                    new Compensation
                    {
                        Id = 100500,
                        EmployeeId = 200,
                        TenantId = 600
                    }
                });

            var query = new PersonalCompensationsQuery(dbContextMock.Object);

            var queryResult = await query.GetPersonalCompensationsAsync(200, 600);

            Assert.Single(queryResult);
            Assert.Equal(100500, queryResult.Single().Id);
        }
    }
}
