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

        //[Fact]
        //public async Task ShouldFindProductWithSeveralBarCodes()
        //{
        //    var expectedProductId = _trackedProducts[1].Id;
        //    ;
        //    var product = await _query.FindAsync(2000000027265, 1);

        //    Assert.NotNull(product);
        //    Assert.Equal(expectedProductId, product.Id);

        //    product = await _query.FindAsync(2207280000000, 1);

        //    Assert.NotNull(product);
        //    Assert.Equal(expectedProductId, product.Id);
        //}
    }
}
