using Core.UserAggregate;

namespace Auth.Interfaces;

public interface ITokenService
{
  public string GetAuthToken(User user);
}
