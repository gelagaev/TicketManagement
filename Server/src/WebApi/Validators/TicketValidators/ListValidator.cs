using Core;
using FluentValidation;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Validators.TicketValidators;

internal sealed class ListValidator : AbstractValidator<TicketListCommand>
{
  public ListValidator(ITicketPermissionAccessService permissionAccessService)
  {
    RuleFor(_ => _)
      .MustAsync(async (_, ct) => await permissionAccessService.CurrentUserCanGetAllTickets(ct))
      .WithMessage(nameof(ErrorCodes.GET_ALL_TICKETS_ACCESS_DENIED));
  } 
}
