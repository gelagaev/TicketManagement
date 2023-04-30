using Auth.Endpoints.Register;
using Auth.Endpoints.Register.V1;
using Auth.Interfaces;
using Core;
using FluentValidation;

namespace Auth.Validators;

/// <summary>
/// Validator for <see cref="RegisterRequest"/>
/// </summary>
internal sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator(IEmailIsTakenProvider emailIsTakenProvider)
  {
    RuleFor(command => command.Password)
      .NotEmpty()
      .MinimumLength(6);

    RuleFor(request => request.Email)
      .EmailAddress()
      .MustAsync(async (_, email, _) => !await emailIsTakenProvider.IsTakenAsync(email))
      .WithMessage(Enum.GetName(ErrorCodes.EMAIL_IS_TAKEN));

    RuleFor(request => request.FirstName)
      .NotEmpty();

    RuleFor(request => request.LastName)
      .NotEmpty();
  }
}
