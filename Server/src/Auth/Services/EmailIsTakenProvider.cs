using Auth.Interfaces;
using Core.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace Auth.Services;

internal sealed class EmailIsTakenProvider : IEmailIsTakenProvider
{
  private readonly UserManager<User> _userManager;

  public EmailIsTakenProvider(UserManager<User> userManager)
  {
    _userManager = userManager;
  }

  public async Task<bool> IsTakenAsync(string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }
}
