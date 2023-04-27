using Auth.Endpoints.Register;
using Auth.Endpoints.SingIn;
using Core.UserAggregate;

namespace Auth.Interfaces;

/// <summary>
/// User manipulating service
/// </summary>
internal interface IUserService
{
  /// <summary>
  /// Registers user
  /// </summary>
  /// <param name="request">Register command</param>
  /// <returns>Register Response<see cref="RegisterResponse"/></returns>
  Task<RegisterResponse> RegisterUserAsync(RegisterCommand request);
}
