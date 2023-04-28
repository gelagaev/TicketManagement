using Ardalis.ApiEndpoints;
using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Core.UserAggregate;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class CreateComment : EndpointBaseAsync
  .WithRequest<CreateCommentRequest>
  .WithActionResult
{
  private readonly IRepository<Ticket> _repository;
  private readonly UserManager<User> _userManager;
  private readonly IHttpContextAccessor _contextAccessor;

  public CreateComment(IRepository<Ticket> repository, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
  {
    _repository = repository;
    _userManager = userManager;
    _contextAccessor = contextAccessor;
  }

  [Authorize]
  [HttpPost(CreateCommentRequest.Route)]
  [SwaggerOperation(
    Summary = "Creates a new Comment for a Ticket",
    Description = "Creates a new Comment for a Ticket",
    OperationId = "Ticket.CreateComment",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    CreateCommentRequest request,
    CancellationToken ct = new())
  {
    var spec = new TicketByIdWithCommentsSpec(request.TicketId);
    var entity = await _repository.FirstOrDefaultAsync(spec, ct);
    if (entity == null)
    {
      return NotFound();
    }

    var userId = _userManager.GetUserId(_contextAccessor.HttpContext!.User);
    var newComment = new Comment()
    {
      CommentText = request.CommentText!,
      UserId = Guid.Parse(userId),
    };
    
    entity.AddComment(newComment);
    await _repository.UpdateAsync(entity, ct);

    return Created(GetTicketByIdRequest.BuildRoute(request.TicketId), null);
  }
}
