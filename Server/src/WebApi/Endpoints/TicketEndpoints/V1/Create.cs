using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Create : EndpointBaseAsync
  .WithRequest<CreateTicketRequest>
  .WithActionResult<CreateTicketResponse>
{
  private readonly IMediator _mediator;

  public Create(IMediator mediator) => _mediator = mediator;

  [HttpPost(CreateTicketRequest.Route)]
  [ApiVersion("1.0")]
  [Authorize]
  [SwaggerOperation(
    Summary = "Creates a new Ticket",
    Description = "Creates a new Ticket",
    OperationId = "Ticket.Create",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<CreateTicketResponse>> HandleAsync(
    CreateTicketRequest request,
    CancellationToken ct = new())
  {
    var response = await _mediator.Send(request, ct);

    return Ok(response);
  }
}
