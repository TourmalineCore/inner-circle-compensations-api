namespace Application.Dtos;

public readonly struct CompensationCreateDto
{
    public List<CompensationDto> Compensations { get; init; }

    public string CompensationRequestedForYearAndMonth { get; init; }
}

public readonly struct CompensationDto
{
    public long TypeId { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public bool IsPaid { get; init; }
}
