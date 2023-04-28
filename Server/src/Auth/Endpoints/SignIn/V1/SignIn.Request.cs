using System.ComponentModel.DataAnnotations;

namespace Auth.Endpoints.SignIn.V1;

/// <summary>
/// Sign In request
/// </summary>
public sealed class SignInRequest
{
  [Required]
  public string Email { get; init; } = string.Empty;

  [Required]
  [MinLength(6)]
  public string Password { get; init; } = string.Empty;
}
