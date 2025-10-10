using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace Api;

public class UserClaimsProvider : IUserClaimsProvider
{
  public const string PermissionClaimType = "permissions";

  public const string CanRequestCompensations = "CanRequestCompensations";
  public const string CanManageCompensations = "CanManageCompensations";
  public const string IsCompensationsHardDeleteAllowed = "IsCompensationsHardDeleteAllowed";

  public Task<List<Claim>> GetUserClaimsAsync(string login)
  {
    throw new NotImplementedException();
  }
}
