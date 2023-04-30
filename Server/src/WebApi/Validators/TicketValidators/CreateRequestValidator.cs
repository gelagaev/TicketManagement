using FluentValidation;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Validators.TicketValidators;

internal sealed class CreateRequestValidator : AbstractValidator<CreateTicketRequest>
{
  public CreateRequestValidator(ITicketPermissionAccessService ticketPermissionAccessService)
  {
    RuleFor(request => request.Description)
      .NotEmpty()
      .MaximumLength(1000);
    RuleFor(request => request.Subject)
      .NotEmpty()
      .MaximumLength(100);
  }
}
