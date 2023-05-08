using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Update : EndpointBaseAsync
  .WithRequest<UpdateTicketRequest>
  .WithActionResult<UpdateTicketResponse>
{
  private readonly IMediator _mediator;

  public Update(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
  [HttpPut(UpdateTicketRequest.Route)]
  [SwaggerOperation(
    Summary = "Updates a Ticket",
    Description = "Updates a Ticket. Only supports changing the subject and description.",
    OperationId = "Tickets.Update",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<UpdateTicketResponse>> HandleAsync(
    UpdateTicketRequest request,
    CancellationToken ct = new())
  {
    var response = await _mediator.Send(request, ct);

    return Ok(response);
  }
}
