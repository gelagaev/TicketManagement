using Auth.Endpoints.SignIn;
using Auth.Interfaces;
using Core.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Handlers;

public class SignInHandler : IRequestHandler<SignInCommand, SignInResponse>
{
  private readonly UserManager<User> _userManager;
  private readonly ITokenService _tokenService;

  public SignInHandler(UserManager<User> userManager, ITokenService tokenService)
  {
    _userManager = userManager;
    _tokenService = tokenService;
  }

  public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken ct)
  {
    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user == null || user.IsActive == false || !await _userManager.CheckPasswordAsync(user, request.Password))
    {
      return new SignInResponse { Error = Enum.GetName(ErrorCodes.LOGIN_FAILED) };
    }

    return new SignInResponse { Success = true, Token = _tokenService.GetAuthToken(user) };
  }
}
