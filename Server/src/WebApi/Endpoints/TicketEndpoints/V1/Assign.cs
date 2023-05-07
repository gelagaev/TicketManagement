using Ardalis.ApiEndpoints;
using Core.UserAggregate.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Assign : EndpointBaseAsync
  .WithRequest<AssignTicketRequest>
  .WithoutResult
{
  private readonly IMediator _mediator;

  public Assign(IMediator mediator) => _mediator = mediator;

  [Authorize(Roles = nameof(Roles.Administrator))]
  [ApiVersion("1.0")]
  [HttpPut(AssignTicketRequest.Route)]
  [SwaggerOperation(
    Summary = "Assign Ticket to Manager",
    Description = "Assign Ticket to Manager",
    OperationId = "Tickets.Assign",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute]AssignTicketRequest ticketRequest,
    CancellationToken ct = new())
  {
    return await _mediator.Send(ticketRequest, ct);
  }
}
