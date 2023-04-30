using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Delete : EndpointBaseAsync
  .WithRequest<DeleteTicketRequest>
  .WithoutResult
{
  private readonly IMediator _mediator;

  public Delete(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
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
    return await _mediator.Send(request, ct);
  }
}
