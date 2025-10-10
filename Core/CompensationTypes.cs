namespace Core;

public static class CompensationTypes
{
  public const string English = "English";
  public const string German = "German";
  public const string Swimming = "Swimming";
  public const string Water = "Water";
  public const string Coworking = "Coworking";
  public const string Massage = "Massage";
  public const string Products = "Products";
  public const string Consumables = "Consumables";
  public const string Periphery = "Periphery";
  public const string BusinessTrip = "Business trip";
  public const string Psychotherapy = "Psychotherapy";
  public const string MedicalConsultation = "Medical consultation";
  public const string Other = "Other";
  public const string Sport = "Sport";
  public const string OfficeExpenses = "Office expenses";

  public static List<CompensationType> GetTypeList()
  {
    return new List<CompensationType>
    {
      new CompensationType(1, English),
      new CompensationType(2, German),
      new CompensationType(3, Swimming),
      new CompensationType(4, Water),
      new CompensationType(5, Coworking),
      new CompensationType(6, Massage),
      new CompensationType(7, Products),
      new CompensationType(8, Consumables),
      new CompensationType(9, Periphery),
      new CompensationType(10, BusinessTrip),
      new CompensationType(11, Psychotherapy),
      new CompensationType(12, MedicalConsultation),
      new CompensationType(13, Other),
      new CompensationType(14, Sport),
      new CompensationType(15, OfficeExpenses)
    };
  }

  public static bool IsTypeExist(long typeId)
  {
    return GetTypeList()
      .Select(x => x.TypeId)
      .Contains(typeId);
  }

  public static string GetTypeNameByTypeId(long typeId)
  {
    if (IsTypeExist(typeId))
    {
      var type = GetTypeList().FirstOrDefault(x => x.TypeId == typeId);
      return type?.Label;
    }
    else
    {
      throw new ArgumentException($"Compensation type [{typeId}] doesn't exists");
    }
  }
}
