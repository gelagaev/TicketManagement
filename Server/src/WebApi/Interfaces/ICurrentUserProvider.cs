using Core.UserAggregate;

namespace WebApi.Interfaces;

internal interface ICurrentUserProvider
{
  Task<User> GetUserAsync();
  Guid GetUserId();
}
