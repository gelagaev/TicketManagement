using Ardalis.Specification;

namespace Core.UserAggregate.Specifications;

public class UserByIdWithRolesSpec : Specification<User>, ISingleResultSpecification<User>
{
  public UserByIdWithRolesSpec(Guid userId)
  {
    Query.Where(user => user.Id == userId)
      .Include(user => user.UserRoles)
      .ThenInclude(role => role.Role);
  }
}
