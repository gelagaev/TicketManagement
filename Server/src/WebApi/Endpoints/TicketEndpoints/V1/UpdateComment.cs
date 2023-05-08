using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class UpdateComment : EndpointBaseAsync
  .WithRequest<UpdateTicketCommentRequest>
  .WithActionResult<UpdateTicketCommentResponse>
{
  private readonly IMediator _mediator;

  public UpdateComment(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
  [HttpPut(UpdateTicketCommentRequest.Route)]
  [SwaggerOperation(
    Summary = "Updates a Ticket Comment",
    Description = "Updates a Ticket Comment. Only supports changing the comment text",
    OperationId = "Tickets.CommentUpdate",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<UpdateTicketCommentResponse>> HandleAsync(
    UpdateTicketCommentRequest request,
    CancellationToken ct = new())
  {
    var response = await _mediator.Send(request, ct);

    return Ok(response);
  }
}
