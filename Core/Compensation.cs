using NodaTime;

namespace Core;

public class Compensation
{
    public long Id { get; set; }

    public long TypeId { get; set; }

    public string? Comment { get; set; }

    public double Amount { get; set; }

    public bool IsPaid { get; set; }

    public long EmployeeId { get; set; }

    public long TenantId { get; set; }

    public Instant CompensationRequestedAtUtc { get; set; }

    public Instant CompensationRequestedForYearAndMonth { get; set; }

    public Compensation() {
    }

    public Compensation(long typeId, string? comment, double amount, Employee employee, string compensationRequestedForYearAndMonth, bool isPaid = false)
    {
        if (!CompensationTypes.IsTypeExist(typeId))
        {
            throw new ArgumentException($"Compensation type [{typeId}] doesn't exists");
        }

        if (amount <= 0)
        {
            throw new ArgumentException($"Amount can't be zero or negative");
        }

        TypeId = typeId;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        EmployeeId = employee.Id;
        TenantId = employee.TenantId;
        CompensationRequestedAtUtc = SystemClock.Instance.GetCurrentInstant();
        CompensationRequestedForYearAndMonth = Instant.FromDateTimeUtc(DateTime.SpecifyKind(DateTime.Parse(compensationRequestedForYearAndMonth), DateTimeKind.Utc));
    }

}
