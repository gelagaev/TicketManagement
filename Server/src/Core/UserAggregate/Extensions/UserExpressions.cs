using System.Linq.Expressions;
using Core.UserAggregate.Enums;

namespace Core.UserAggregate.Extensions;

internal static class UserExpressions
{
  public static readonly Expression<Func<User, bool>> IsAdministrator =
    user => user.Roles.Any(r => r.Role.Name == Roles.Administrator.ToString());

  public static readonly Expression<Func<User, bool>> IsManager =
    user => user.Roles.Any(r => r.Role.Name == Roles.Manager.ToString());

  public static readonly Expression<Func<User, bool>> IsClient =
    user => user.Roles.Any(r => r.Role.Name == Roles.Client.ToString());
}
