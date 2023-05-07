using Ardalis.Specification;
using Core.UserAggregate.Enums;

namespace Core.UserAggregate.Specifications;

public class UserWithManagerRoleSpec : Specification<User>, ISingleResultSpecification<User>
{
  public UserWithManagerRoleSpec()
  {
    Query
      .Where(user => user.UserRoles.Any(role => role.Role.Name == nameof(Roles.Manager)))
      .Include(user => user.UserRoles)
      .ThenInclude(role => role.Role);
  }
}
