using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class List : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<TicketListResponse>
{
  private readonly IMediator _mediator;
  public List(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
  [HttpGet(TicketListCommand.Route)]
  [SwaggerOperation(
    Summary = "Gets a list of all Tickets",
    Description = "Gets a list of all Tickets",
    OperationId = "Ticket.List",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<TicketListResponse>> HandleAsync(CancellationToken ct = new())
  {
    var response = await _mediator.Send(new TicketListCommand(), ct);

    return Ok(response);
  }
}
