using Microsoft.AspNetCore.Identity;

namespace Auth.Endpoints.Register;

/// <summary>
/// Register response
/// </summary>
public sealed class RegisterResponse
{
  public bool Success { get; init; }
  public IEnumerable<IdentityError>? Errors { get; init; }
}
