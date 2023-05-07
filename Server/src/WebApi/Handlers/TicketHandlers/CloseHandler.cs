using Ardalis.GuardClauses;
using Core.TicketAggregate;
using Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class CloseHandler : IRequestHandler<CloseTicketRequest, ActionResult>
{
  private readonly IRepository<Ticket> _repository;

  public CloseHandler(IRepository<Ticket> repository) => _repository = repository;

  public async Task<ActionResult> Handle(CloseTicketRequest request, CancellationToken ct)
  {
    var existingTicket = await _repository.GetByIdAsync(request.TicketId, ct);
    Guard.Against.Null(existingTicket, nameof(existingTicket));

    existingTicket.Close();

    await _repository.UpdateAsync(existingTicket, ct);
    return new OkResult();
  }
}
