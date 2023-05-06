using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class DeleteComment : EndpointBaseAsync
  .WithRequest<DeleteTicketCommentRequest>
  .WithoutResult
{
  private readonly IMediator _mediator;

  public DeleteComment(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
  [HttpDelete(DeleteTicketCommentRequest.Route)]
  [SwaggerOperation(
    Summary = "Deletes a Comment",
    Description = "Deletes a Comment",
    OperationId = "Tickets.DeleteComment",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteTicketCommentRequest request,
    CancellationToken ct = new())
  {
    return await _mediator.Send(request, ct);
  }
}
