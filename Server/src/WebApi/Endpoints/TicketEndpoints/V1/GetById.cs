using Ardalis.ApiEndpoints;
using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class GetById : EndpointBaseAsync
  .WithRequest<GetTicketByIdRequest>
  .WithActionResult<GetTicketByIdResponse>
{
  private readonly IRepository<Ticket> _repository;

  public GetById(IRepository<Ticket> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetTicketByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a single Ticket",
    Description = "Gets a single Ticket by Id",
    OperationId = "Ticket.GetById",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<GetTicketByIdResponse>> HandleAsync(
    [FromRoute] GetTicketByIdRequest request,
    CancellationToken ct = new())
  {
    var spec = new TicketByIdWithCommentsSpec(request.TicketId);
    var entity = await _repository.FirstOrDefaultAsync(spec, ct);
    if (entity == null)
    {
      return NotFound();
    }

    var response = new GetTicketByIdResponse
    (
      id: entity.Id,
      subject: entity.Subject,
      description: entity.Description,
      comments: entity.Comments.Select(
        item => new CommentRecord(item.Id, 
          item.CommentText,
          item.UserId))
        .ToList()
    );

    return Ok(response);
  }
}
