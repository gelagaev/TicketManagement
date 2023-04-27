namespace Auth.Endpoints.Register;

/// <summary>
/// Register request
/// </summary>
public sealed class RegisterRequest
{
  internal const string Route = "/Register";
  public string Email { get; init; } = string.Empty;
  public string Password { get; init; } = string.Empty;
  public string FirstName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
}
