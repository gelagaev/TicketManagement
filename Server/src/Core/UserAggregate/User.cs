using Core.UserAggregate.Extensions;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.UserAggregate;

public class User : IdentityUser<Guid>, IAggregateRoot
{
  public bool IsActive { get; init; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
  public bool IsAdministrator => UserExpressions.IsAdministrator.Compile()(this);
  public bool IsManager => UserExpressions.IsManager.Compile()(this);
  public bool IsClient => UserExpressions.IsClient.Compile()(this);
  public bool IsAdministratorOrManager => UserExpressions.IsAdministratorOrManager.Compile()(this);
}
