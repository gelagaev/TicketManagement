using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Close : EndpointBaseAsync
  .WithRequest<CloseTicketRequest>
  .WithActionResult
{
  private readonly IMediator _mediator;

  public Close(IMediator mediator) => _mediator = mediator;
  [Authorize()]
  [ApiVersion("1.0")]
  [HttpPut(CloseTicketRequest.Route)]
  [SwaggerOperation(
    Summary = "Closes a Ticket",
    Description = "Closes a Ticket",
    OperationId = "Tickets.Close",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    CloseTicketRequest request,
    CancellationToken ct = new())
  {
    return await _mediator.Send(request, ct);
  }
}
