namespace Auth.DTO;

/// <summary>
/// Login response
/// </summary>
public sealed class LoginResponse
{
  public string Token { get; init; } = string.Empty;
}
