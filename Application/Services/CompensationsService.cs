using Application.Commands;
using Application.Dtos;
using Application.Queries.Contracts;
using Core;

namespace Application.Services;

public class CompensationsService
{
  private readonly CompensationCreationCommand _compensationCreationCommand;

  private readonly CompensationStatusUpdateCommand _compensationStatusUpdateCommand;

  private readonly ICompensationsQuery _compensationsQuery;

  private readonly IPersonalCompensationsQuery _personalcompensationsQuery;

  public CompensationsService(
    CompensationCreationCommand createCompensationCommandHandler,
    ICompensationsQuery compensationsQuery,
    IPersonalCompensationsQuery personalcompensationsQuery,
    CompensationStatusUpdateCommand compensationStatusUpdateCommand
  )
  {
    _compensationCreationCommand = createCompensationCommandHandler;
    _compensationsQuery = compensationsQuery;
    _compensationStatusUpdateCommand = compensationStatusUpdateCommand;
    _personalcompensationsQuery = personalcompensationsQuery;
  }

  public static List<CompensationType> GetTypes()
  {
    return CompensationTypes.GetTypeList();
  }

  public async Task<PersonalCompensationListDto> GetEmployeeCompensationsAsync(Employee employee, long tenantId)
  {
    var compensations = await _personalcompensationsQuery.GetPersonalCompensationsAsync(employee.Id, tenantId);

    return new PersonalCompensationListDto(compensations);
  }

  public async Task<List<long>> CreateAsync(CompensationCreateDto dto, Employee employee)
  {
    return await _compensationCreationCommand.ExecuteAsync(dto, employee);
  }

  public async Task<AllCompensationsListDto> GetAdminAllAsync(int year, int month, List<Employee> employees, long tenantId)
  {
    var compensations = await _compensationsQuery.GetCompensationsAsync(year, month, tenantId);

    return new AllCompensationsListDto(compensations, employees);
  }

  public async Task UpdateStatusAsync(long[] compensationsIds)
  {
    var compensations = await _compensationsQuery.GetCompensationsByIdsAsync(compensationsIds);

    foreach (var compensation in compensations.Where(compensation => !compensation.IsPaid))
    {
      compensation.IsPaid = true;
    }

    await _compensationStatusUpdateCommand.ExecuteAsync(compensations);
  }
}
