using Auth.Endpoints.SignIn;
using FluentValidation;

namespace Auth.Validators;

/// <summary>
/// Validator for <see cref="SignInCommand"/>
/// </summary>
internal sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
  public SignInCommandValidator()
  {
    RuleFor(request => request.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(request => request.Password)
      .NotEmpty()
      .MinimumLength(6);
  }
}
