using Core;
using FluentValidation;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Validators.TicketValidators;

internal sealed class GetByIdValidator : AbstractValidator<GetTicketByIdRequest>
{
  public GetByIdValidator(ITicketPermissionAccessService permissionAccessService)
  {
    RuleFor(request => request.TicketId)
      .MustAsync(async (ticketId, ct) => await permissionAccessService.CurrentUserCanGetTicket(ticketId, ct))
      .WithMessage(nameof(ErrorCodes.GET_TICKET_ACCESS_DENIED));
  }
}
