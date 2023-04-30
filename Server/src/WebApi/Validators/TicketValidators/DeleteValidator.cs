using Core;
using FluentValidation;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Validators.TicketValidators;

internal sealed class DeleteValidator : AbstractValidator<DeleteTicketRequest>
{
  public DeleteValidator(ITicketPermissionAccessService ticketPermissionAccessService)
  {
    RuleFor(request => request.TicketId)
      .NotEmpty()
      .MustAsync(async (ticketId, ct) => await ticketPermissionAccessService.CurrentUserCanDeleteTicketAsync(ticketId, ct))
      .WithMessage(nameof(ErrorCodes.DELETE_TICKET_ACCESS_DENIED));
  }
}
