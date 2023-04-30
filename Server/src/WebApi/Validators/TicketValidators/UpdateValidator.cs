using FluentValidation;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Validators.TicketValidators;

internal sealed class UpdateValidator : AbstractValidator<UpdateTicketRequest>
{
  public UpdateValidator(ITicketPermissionAccessService permissionAccessService)
  {
    RuleFor(request => request.Description)
      .NotEmpty()
      .MaximumLength(1000);
    RuleFor(request => request.Subject)
      .NotEmpty()
      .MaximumLength(100);
    RuleFor(request => request.Id)
      .MustAsync(async (ticketId, ct) => await permissionAccessService.CurrentUserCanUpdateTicket(ticketId, ct));
  }
}
