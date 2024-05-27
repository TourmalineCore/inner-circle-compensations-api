using Core;

namespace Application.Dtos;

public class AllCompensationsListDto
{
    public double TotalAmount { get; }

    public double TotalUnpaidAmount { get; }

    public IEnumerable<ItemDto> Items { get; }

    public AllCompensationsListDto(IEnumerable<Compensation> compensations, List<Employee> employees)
    {
        Items = compensations.GroupBy(x => x.EmployeeId)
                     .Select(x => new ItemDto(x.ToList(), employees.FirstOrDefault(employee => employee.Id == x.Key)));
        TotalAmount = Math.Round(compensations.Sum(x => x.Amount), 2);
        TotalUnpaidAmount = Math.Round(compensations.Where(x => !x.IsPaid).Sum(x => x.Amount), 2);
    }
}

public class ItemDto
{
    public string EmployeeFullName { get; }

    public long EmployeeId { get; }

    public string CompensationRequestedForYearAndMonth { get; }

    public double TotalAmount { get; }

    public double UnpaidAmount { get; }

    public bool IsPaid { get; }

    public IEnumerable<EmployeeCompensationDto> Compensations { get; }

    public ItemDto(List<Compensation> employeeCompensations, Employee employee)
    {
        EmployeeFullName = employee.FullName;
        CompensationRequestedForYearAndMonth = employeeCompensations[0].CompensationRequestedForYearAndMonth.ToString();
        TotalAmount = Math.Round(employeeCompensations.Sum(x => x.Amount), 2);
        UnpaidAmount = Math.Round(employeeCompensations.Where(x => !x.IsPaid).Sum(x => x.Amount), 2);
        Compensations = employeeCompensations.Select(x => new EmployeeCompensationDto(x));
        EmployeeId = employee.Id;
        IsPaid = employeeCompensations.All(x => x.IsPaid == true);
    }
}

public class EmployeeCompensationDto
{
    public long Id { get; }

    public string CompensationType { get; }

    public string? Comment { get; }

    public double Amount { get; }

    public string CompensationRequestedAtUtc { get; }

    public EmployeeCompensationDto(Compensation compensation)
    {
        Id = compensation.Id;
        CompensationType = CompensationTypes.GetTypeNameByTypeId(compensation.TypeId);
        Comment = compensation.Comment;
        Amount = compensation.Amount;
        CompensationRequestedAtUtc = compensation.CompensationRequestedAtUtc.ToString();
    }
}