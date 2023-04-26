using Auth.DTO;
using FluentValidation;

namespace Auth.Validators;

/// <summary>
/// Validator for AuthRequest
/// </summary>
internal sealed class AuthRequestValidator : AbstractValidator<LoginRequest>
{
  public AuthRequestValidator()
  {
    RuleFor(request => request.Login).NotEmpty();
    RuleFor(request => request.Password).NotEmpty().MinimumLength(6);
  }
}
