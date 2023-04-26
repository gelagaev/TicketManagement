namespace Auth.DTO;

/// <summary>
/// Login request
/// </summary>
public sealed class LoginRequest
{
  public string Login { get; init; } = string.Empty;
  public string Password { get; init; } = string.Empty;
}
