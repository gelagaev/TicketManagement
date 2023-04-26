using Auth.DTO;
using FluentValidation;

namespace Auth.Validators;

/// <summary>
/// Validator for RegisterRequest
/// </summary>
internal sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator()
  {
    RuleFor(request => request.Password).NotEmpty().MinimumLength(6);
    RuleFor(request => request.Email).EmailAddress();
  }
}
