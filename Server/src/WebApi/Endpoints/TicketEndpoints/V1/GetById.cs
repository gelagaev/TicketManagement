using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class GetById : EndpointBaseAsync
  .WithRequest<GetTicketByIdRequest>
  .WithActionResult<GetTicketByIdResponse>
{
  private readonly IMediator _mediator;

  public GetById(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
  [HttpGet(GetTicketByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a single Ticket",
    Description = "Gets a single Ticket by Id",
    OperationId = "Ticket.GetById",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<GetTicketByIdResponse>> HandleAsync(
    [FromRoute] GetTicketByIdRequest request, CancellationToken ct = new())
  {
    var response = await _mediator.Send(request, ct);

    return Ok(response);
  }
}
