using Core;

namespace Application.Dtos;

public class PersonalCompensationListDto
{
    public List<PersonalCompensationItemDto> List { get; }

    public double TotalUnpaidAmount { get; }

    public PersonalCompensationListDto(IEnumerable<Compensation> compensations)
    {
        List = compensations.Select(x => new PersonalCompensationItemDto(x.Id, x.Comment, x.Amount, x.IsPaid, x.TypeId, x.CompensationRequestedAtUtc.ToString(), x.CompensationRequestedForYearAndMonth.ToString())).ToList();
        TotalUnpaidAmount = Math.Round(compensations.Where(compensation => compensation.IsPaid == false).Sum(x => x.Amount), 2);
    }
}

public class PersonalCompensationItemDto
{
    public long Id { get; }

    public string? Comment { get; }

    public double Amount { get; }

    public bool IsPaid { get; }

    public string CompensationType { get; }

    public string CompensationRequestedAtUtc { get; }

    public string CompensationRequestedForYearAndMonth { get; }

    public PersonalCompensationItemDto(long id, string? comment, double amount, bool isPaid, long typeId, string CompensationRequestedAtUtc, string CompensationRequestedForYearAndMonth)
    {
        Id = id;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        CompensationType = CompensationTypes.GetTypeNameByTypeId(typeId);
        CompensationRequestedAtUtc = CompensationRequestedAtUtc;
        CompensationRequestedForYearAndMonth = CompensationRequestedForYearAndMonth;
    }
}