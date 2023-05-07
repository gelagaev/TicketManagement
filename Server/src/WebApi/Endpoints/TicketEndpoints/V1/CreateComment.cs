using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class CreateComment : EndpointBaseAsync
  .WithRequest<CreateCommentRequest>
  .WithActionResult<CreateCommentResponse>
{
  private readonly IMediator _mediator;

  public CreateComment(IMediator mediator) => _mediator = mediator;

  [Authorize]
  [ApiVersion("1.0")]
  [HttpPost(CreateCommentRequest.Route)]
  [SwaggerOperation(
    Summary = "Creates a new Comment for a Ticket",
    Description = "Creates a new Comment for a Ticket",
    OperationId = "Ticket.CreateComment",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<CreateCommentResponse>> HandleAsync(
    CreateCommentRequest request,
    CancellationToken ct = new())
  {
    var response = await _mediator.Send(request, ct);
    return response;
  }
}
