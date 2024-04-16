using Core;

namespace Application;

public interface IInnerCircleHttpClient
{
    Task<Employee> GetEmployeeAsync(string corporateEmail);
    Task<List<Employee>> GetEmployeesAsync(long tenantId);
}
