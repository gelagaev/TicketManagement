using Auth.Endpoints.Register;
using Auth.Endpoints.Register.V1;
using Auth.Interfaces;
using MediatR;

namespace Auth.Handlers;

internal sealed class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
  private readonly IUserService _userService;

  public RegisterHandler(IUserService userService) => _userService = userService;

  public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken ct)
  {
    return await _userService.RegisterUserAsync(request);
  }
}
