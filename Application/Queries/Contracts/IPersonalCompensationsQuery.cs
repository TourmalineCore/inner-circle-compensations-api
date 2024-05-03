using Core;

namespace Application.Queries.Contracts;

public interface IPersonalCompensationsQuery
{
    Task<List<Compensation>> GetPersonalCompensationsAsync(long employeeId, long tenantId);
}
