﻿namespace Auth.Endpoints.Register.V1;

/// <summary>
/// Register request
/// </summary>
public sealed class RegisterRequest
{
  public string Email { get; init; } = string.Empty;
  public string Password { get; init; } = string.Empty;
  public string FirstName { get; init; } = string.Empty;
  public string LastName { get; init; } = string.Empty;
}
