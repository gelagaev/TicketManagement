using Auth.Endpoints.Register;
using Auth.Interfaces;
using FluentValidation;

namespace Auth.Validators;

/// <summary>
/// Validator for <see cref="RegisterRequest"/>
/// </summary>
internal sealed class RegisterRequestValidator : AbstractValidator<RegisterCommand>
{
  public RegisterRequestValidator(IEmailIsTakenProvider emailIsTakenProvider)
  {
    RuleFor(command => command.Password)
      .NotEmpty()
      .MinimumLength(6);

    RuleFor(command => command.Email)
      .EmailAddress()
      .MustAsync(async (_, email, _) => !await emailIsTakenProvider.IsTakenAsync(email))
      .WithMessage(Enum.GetName(ErrorCodes.EMAIL_IS_TAKEN));

    RuleFor(command => command.FirstName)
      .NotEmpty();

    RuleFor(command => command.LastName)
      .NotEmpty();
  }
}
