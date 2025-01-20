using Core;
using System.ComponentModel;

namespace Application.Dtos;

public class PersonalCompensationListDto
{
    public List<PersonalCompensationItemDto> List { get; }

    public double TotalUnpaidAmount { get; }

    public PersonalCompensationListDto(IEnumerable<Compensation> compensations)
    {
        List = compensations.Select(x => new PersonalCompensationItemDto(
            x.Id, 
            x.Comment, 
            x.Amount, 
            x.IsPaid, 
            x.TypeId, 
            x.CompensationRequestedAtUtc.ToString(), 
            x.CompensationRequestedForYearAndMonth.ToString(),
            x.Quantity))
            .ToList();
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
    
    [DefaultValue(1)]
    public int Quantity { get; }

    public PersonalCompensationItemDto(
        long id, 
        string? comment, 
        double amount, 
        bool isPaid, 
        long typeId, 
        string compensationRequestedAtUtc, 
        string compensationRequestedForYearAndMonth,
        int quantity)
    {
        Id = id;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        CompensationType = CompensationTypes.GetTypeNameByTypeId(typeId);
        CompensationRequestedAtUtc = compensationRequestedAtUtc;
        CompensationRequestedForYearAndMonth = compensationRequestedForYearAndMonth;
        Quantity = quantity;
    }
}