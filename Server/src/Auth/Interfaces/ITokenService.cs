using Core.UserAggregate;

namespace Auth.Interfaces;

/// <summary>
/// Jwt token service
/// </summary>
public interface ITokenService
{
  /// <summary>
  /// Generates Jwt token for user
  /// </summary>
  /// <param name="user">User <see cref="User"/></param>
  /// <returns>Token</returns>
  public string GetAuthToken(User user);
}
