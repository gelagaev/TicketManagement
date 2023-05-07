using Ardalis.GuardClauses;
using Core.TicketAggregate;
using Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class AssignHandler : IRequestHandler<AssignTicketRequest, ActionResult>
{
  private readonly IRepository<Ticket> _repository;

  public AssignHandler(IRepository<Ticket> repository) => _repository = repository;

  public async Task<ActionResult> Handle(AssignTicketRequest request, CancellationToken ct)
  {
    var existingTicket = await _repository.GetByIdAsync(request.TicketId, ct);
    Guard.Against.Null(existingTicket, nameof(existingTicket));

    existingTicket.AssignToUser(request.ManagerId);

    await _repository.UpdateAsync(existingTicket, ct);
    return new OkResult();
  }
}
