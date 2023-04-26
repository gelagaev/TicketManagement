using Auth.DTO;
using Auth.Interfaces;
using Core.UserAggregate;
using Core.UserAggregate.Enums;
using Microsoft.AspNetCore.Identity;

namespace Auth.Services;

/// <summary>
/// User manipulating service
/// </summary>
internal sealed class UserService : IUserService
{
  private readonly UserManager<User> _userManager;
  private readonly ITokenService _tokenService;
  private readonly RoleManager<Role> _roleManager;

  public UserService(
    UserManager<User> userManager,
    ITokenService tokenService,
    RoleManager<Role> roleManager
  )
  {
    _userManager = userManager;
    _tokenService = tokenService;
    _roleManager = roleManager;
  }

  /// <summary>
  /// Returns a flag indicating whether the given password is valid for the specified user.
  /// </summary>
  /// <param name="user">User</param>
  /// <param name="password">Password</param>
  /// <returns></returns>
  public Task<bool> CheckPasswordAsync(User user, string password)
  {
    return _userManager.CheckPasswordAsync(user, password);
  }

  /// <summary>
  /// Finds and returns a user, if any, who has the specified user name
  /// </summary>
  /// <param name="userName">The user name to search for</param>
  /// <returns></returns>
  public Task<User> FindByNameAsync(string userName)
  {
    //TODO null
    return _userManager.FindByNameAsync(userName);
  }

  /// <summary>
  /// Provides access token for valid credentials and null otherwise
  /// </summary>
  /// <param name="request">Auth request</param>
  /// <returns></returns>
  public async Task<string?> GetAuthTokenOnUserLogin(LoginRequest request)
  {
    var user = await FindByNameAsync(request.Login);
    //TODO
    // if (user == null)
    // {
    //   return null;
    // }

    var valid = await CheckPasswordAsync(user, request.Password);

    return valid ? _tokenService.GetAuthToken(user) : null;
  }

  /// <summary>
  /// Registers user
  /// </summary>
  /// <param name="request">Register request</param>
  /// <returns></returns>
  public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
  {
    // var clientRole = _roleManager.Roles.FirstAsync(role => role.Name == Roles.Client.ToString());
    var user = new User
    {
      Id = Guid.NewGuid(),
      IsActive = true,
      Email = request.Email,
      UserName = request.Email,
      // Roles = new List<UserRole> { new() { RoleId = (await clientRole).Id, UserId = userId, } }
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    await _userManager.AddToRoleAsync(user, Roles.Client.ToString());

    return new RegisterResponse { Success = result.Succeeded, Errors = result.Errors.Any() ? result.Errors : null };
  }

  /// <summary>
  /// Adds Administrator role to user
  /// </summary>
  /// <param name="user">User</param>
  public async Task AddAdminRole(User user)
  {
    await _userManager.AddToRoleAsync(user, Roles.Administrator.ToString());
  }
}
