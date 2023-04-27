using MediatR;

namespace Auth.Endpoints.SignIn;

/// <summary>
/// Command to try Sign In
/// </summary>
public class SignInCommand : IRequest<SignInResponse>
{
  public string Email { get; init; } = string.Empty;
  public string Password { get; init; } = string.Empty;
}
