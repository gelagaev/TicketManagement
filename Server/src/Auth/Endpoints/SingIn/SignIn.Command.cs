using MediatR;

namespace Auth.Endpoints.SingIn;

/// <summary>
/// Command to try signIn
/// </summary>
public class SignInCommand : IRequest<SignInResponse>
{
  public string Email { get; init; } = string.Empty;
  public string Password { get; init; } = string.Empty;
}
