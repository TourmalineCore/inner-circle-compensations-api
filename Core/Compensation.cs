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

    public DateOnly CompensationRequestedForYearAndMonth { get; set; }

    public int Quantity { get; set; }

    public Compensation()
    {
    }

    public Compensation(
        long typeId, 
        string? comment, 
        double amount, 
        Employee employee, 
        string compensationRequestedForYearAndMonth, 
        int quantity = 1, 
        bool isPaid = false
        )
    {
        if (!CompensationTypes.IsTypeExist(typeId))
        {
            throw new ArgumentException($"Compensation type [{typeId}] doesn't exists");
        }

        if (amount <= 0)
        {
            throw new ArgumentException($"Amount can't be zero or negative");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException($"Quantity can't be zero or negative");
        }

        TypeId = typeId;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        EmployeeId = employee.Id;
        TenantId = employee.TenantId;
        CompensationRequestedAtUtc = SystemClock.Instance.GetCurrentInstant();
        //CompensationRequestedAtUtc = Instant.FromDateTimeUtc(DateTime.UtcNow);
        var result = DateTime.Parse(compensationRequestedForYearAndMonth, null, System.Globalization.DateTimeStyles.RoundtripKind);
        CompensationRequestedForYearAndMonth = new DateOnly(result.Year, result.Month, 1);
        Quantity = quantity;
    }

}