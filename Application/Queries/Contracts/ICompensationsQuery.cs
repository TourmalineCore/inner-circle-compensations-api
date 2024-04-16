using Core;

namespace Application.Queries.Contracts;

public interface ICompensationsQuery
{
    Task<IEnumerable<Compensation>> GetCompensationsAsync(int year, int month, long tenantId);
    Task<IEnumerable<Compensation>> GetPersonalCompensationsAsync(long employeeId);
    Task<List<Compensation>> GetCompensationsByIdsAsync(long[] ids);
}
