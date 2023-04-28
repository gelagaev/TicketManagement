﻿using Auth.Endpoints.SignIn.V1;
using Auth.Interfaces;
using Core.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Handlers;

public class SignInHandler : IRequestHandler<SignInRequest, SignInResponse>
{
  private readonly UserManager<User> _userManager;
  private readonly ITokenService _tokenService;

  public SignInHandler(UserManager<User> userManager, ITokenService tokenService)
  {
    _userManager = userManager;
    _tokenService = tokenService;
  }

  public async Task<SignInResponse> Handle(SignInRequest request, CancellationToken ct)
  {
    var user = _userManager.Users
      .Include(u => u.Roles)
      .ThenInclude(r => r.Role)
      .FirstOrDefault(u => u.Email == request.Email);

    if (user == null || user.IsActive == false || !await _userManager.CheckPasswordAsync(user, request.Password))
    {
      return new SignInResponse { Error = Enum.GetName(ErrorCodes.LOGIN_FAILED) };
    }

    return new SignInResponse { Success = true, Token = _tokenService.GetAuthToken(user) };
  }
}
