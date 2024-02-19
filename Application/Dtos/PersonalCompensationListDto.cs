using Core;

namespace Application.Dtos;

public class PersonalCompensationListDto
{
    public List<PersonalCompensationItemDto> List { get; }

    public double TotalUnpaidAmount { get; }

    public PersonalCompensationListDto(List<PersonalCompensationItemDto> compensationList, double totalUnpaidAmount)
    {
        List = compensationList;
        TotalUnpaidAmount = totalUnpaidAmount;
    }
}

public class PersonalCompensationItemDto
{
    public long Id { get; }

    public string? Comment { get; }

    public double Amount { get; }

    public bool IsPaid { get; }

    public string CompensationType { get; }

    public string DateCreateCompensation { get; }

    public string DateCompensation { get; }

    public PersonalCompensationItemDto(long id, string? comment, double amount, bool isPaid, long typeId, string dateCreateCompensation, string dateCompensation)
    {
        Id = id;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        CompensationType = CompensationTypes.GetTypeNameByTypeId(typeId);
        DateCreateCompensation = dateCreateCompensation;
        DateCompensation = dateCompensation;
    }
}