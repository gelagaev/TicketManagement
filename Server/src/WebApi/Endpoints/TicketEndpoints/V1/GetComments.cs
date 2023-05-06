using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class GetComments : EndpointBaseAsync
  .WithRequest<GetCommentsRequest>
  .WithActionResult<GetCommentsResponse>
{
  private readonly IMediator _mediator;
  public GetComments(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
  [HttpGet(GetCommentsRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets Comments for a Ticket",
    Description = "Gets Comments for a Ticket",
    OperationId = "Ticket.GetComments",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<GetCommentsResponse>>
    HandleAsync([FromRoute]GetCommentsRequest request, CancellationToken ct = new())
  {
    var response = await _mediator.Send(request);
    return Ok(response);
  }
}
