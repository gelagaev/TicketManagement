using System.ComponentModel.DataAnnotations;

namespace Auth.Endpoints.SingIn;

/// <summary>
/// SignIn request
/// </summary>
public sealed class SignInRequest
{
  internal const string Route = "/SingIn";
  [Required]
  public string Email { get; init; } = string.Empty;

  [Required]
  [MinLength(6)]
  public string Password { get; init; } = string.Empty;
}
