using Core.TicketAggregate;
using Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

public class DeleteHandler : IRequestHandler<DeleteTicketRequest, ActionResult>
{
  private readonly IRepository<Ticket> _repository;

  public DeleteHandler(IRepository<Ticket> repository) => _repository = repository;
  
  public async Task<ActionResult> Handle(DeleteTicketRequest request, CancellationToken ct)
  {
    var aggregateToDelete = await _repository.GetByIdAsync(request.TicketId, ct);
    if (aggregateToDelete == null)
    {
      return new NotFoundResult();
    }

    await _repository.DeleteAsync(aggregateToDelete, ct);

    return new NoContentResult();
  }
}
