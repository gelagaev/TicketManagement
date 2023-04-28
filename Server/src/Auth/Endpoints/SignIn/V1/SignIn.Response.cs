namespace Auth.Endpoints.SignIn.V1;

/// <summary>
/// Login response
/// </summary>
public sealed class SignInResponse
{
  public bool Success { get; init; }
  public string? Token { get; init; }
  public string? Error { get; init; }
}
