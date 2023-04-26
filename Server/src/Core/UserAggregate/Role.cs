using Microsoft.AspNetCore.Identity;

namespace Core.UserAggregate;

public class Role : IdentityRole<Guid>
{
  public virtual List<UserRole> UserRoles { get; init; } = default!;
}
