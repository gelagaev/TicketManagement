using Auth.DTO;
using Core.UserAggregate;

namespace Auth.Interfaces;

public interface IUserService
{
  Task<bool> CheckPasswordAsync(User userName, string password);
  Task<User> FindByNameAsync(string userName);
  Task<string?> GetAuthTokenOnUserLogin(LoginRequest request);
  Task<RegisterResponse> RegisterUser(RegisterRequest request);
  Task AddAdminRole(User user);
}
