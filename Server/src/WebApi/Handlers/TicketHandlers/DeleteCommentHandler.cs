using Core.TicketAggregate;
using Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class DeleteCommentHandler : IRequestHandler<DeleteTicketCommentRequest, ActionResult>
{
  private readonly IRepository<Comment> _repository;

  public DeleteCommentHandler(IRepository<Comment> repository) => _repository = repository;
  
  public async Task<ActionResult> Handle(DeleteTicketCommentRequest request, CancellationToken ct)
  {
    var aggregateToDelete = await _repository.GetByIdAsync(request.CommentId, ct);
    if (aggregateToDelete == null)
    {
      return new NotFoundResult();
    }

    await _repository.DeleteAsync(aggregateToDelete, ct);

    return new NoContentResult();
  }
}
