using Auth.Endpoints.SignIn.V1;
using FluentValidation;

namespace Auth.Validators;

/// <summary>
/// Validator for <see cref="SignInRequest"/>
/// </summary>
internal sealed class SignInRequestValidator : AbstractValidator<SignInRequest>
{
  public SignInRequestValidator()
  {
    RuleFor(request => request.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(request => request.Password)
      .NotEmpty()
      .MinimumLength(6);
  }
}
