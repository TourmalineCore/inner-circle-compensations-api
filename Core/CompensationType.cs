namespace Core;

public class CompensationType
{
    public long TypeId { get; }

    public string Label { get; }

    public CompensationType(long typeId, string label)
    {
        TypeId = typeId;
        Label = label;
    }
}