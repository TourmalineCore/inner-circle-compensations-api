using Core;

namespace Application.Queries.Contracts;

public interface ICompensationsQuery
{
    Task<IEnumerable<Compensation>> GetCompensationsAsync(int year, int month, long tenantId);
    Task<List<Compensation>> GetCompensationsByIdsAsync(long[] ids);
}
