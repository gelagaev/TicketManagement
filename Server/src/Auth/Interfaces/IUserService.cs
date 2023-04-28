using Auth.Endpoints.Register.V1;

namespace Auth.Interfaces;

/// <summary>
/// User manipulating service
/// </summary>
internal interface IUserService
{
  /// <summary>
  /// Registers user
  /// </summary>
  /// <param name="request">Register request</param>
  /// <returns>Register Response<see cref="RegisterResponse"/></returns>
  Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
}
