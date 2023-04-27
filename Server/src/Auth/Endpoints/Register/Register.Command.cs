using MediatR;

namespace Auth.Endpoints.Register;
/// <summary>
/// Register command
/// </summary>
internal sealed class RegisterCommand : IRequest<RegisterResponse>
{
  public string Email { get; init; } = string.Empty;
  public string Password { get; init; } = string.Empty;
  public string FirstName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
}
