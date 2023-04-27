using Auth.Endpoints.Register;
using Auth.Interfaces;
using Core.UserAggregate;
using Core.UserAggregate.Enums;
using Microsoft.AspNetCore.Identity;

namespace Auth.Services;

/// <summary>
/// <inheritdoc />
/// </summary>
internal sealed class UserService : IUserService
{
  private readonly UserManager<User> _userManager;

  public UserService(UserManager<User> userManager) => _userManager = userManager;

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public async Task<RegisterResponse> RegisterUserAsync(RegisterCommand request)
  {
    var user = new User { IsActive = true, Email = request.Email, UserName = request.Email, };

    var result = await _userManager.CreateAsync(user, request.Password);
    await _userManager.AddToRoleAsync(user, Roles.Client.ToString());

    return new RegisterResponse { Success = result.Succeeded, Errors = result.Errors.Any() ? result.Errors : null };
  }
}
