using Core;

namespace Application.Queries.Contracts;

public interface ITenantCompensationsQuery
{
    Task<List<Compensation>> GetCompensationsByTenantIdAsync(long tenantId);
}
