using Ardalis.ApiEndpoints;
using Core.TicketAggregate;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteTicketRequest>
    .WithoutResult
{
  private readonly IRepository<Ticket> _repository;

  public Delete(IRepository<Ticket> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteTicketRequest.Route)]
  [SwaggerOperation(
      Summary = "Deletes a Ticket",
      Description = "Deletes a Ticket",
      OperationId = "Tickets.Delete",
      Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteTicketRequest request,
      CancellationToken ct = new())
  {
    var aggregateToDelete = await _repository.GetByIdAsync(request.TicketId, ct);
    if (aggregateToDelete == null)
    {
      return NotFound();
    }

    await _repository.DeleteAsync(aggregateToDelete, ct);

    return NoContent();
  }
}
