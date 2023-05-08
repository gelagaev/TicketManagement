using Ardalis.GuardClauses;
using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class UpdateCommentHandler : IRequestHandler<UpdateTicketCommentRequest, UpdateTicketCommentResponse>
{
  private readonly IRepository<Comment> _repository;

  public UpdateCommentHandler(IRepository<Comment> repository) => _repository = repository;

  public async Task<UpdateTicketCommentResponse> Handle(UpdateTicketCommentRequest request, CancellationToken ct)
  {
    var spec = new CommentWithAuthorSpec(request.Id);
    var existingComment = await _repository.FirstOrDefaultAsync(spec, ct);
    Guard.Against.Null(existingComment, nameof(existingComment));

    existingComment.UpdateCommentText(request.CommentText);

    await _repository.UpdateAsync(existingComment, ct);

    var response = new UpdateTicketCommentResponse(existingComment.ToRecord(request.Id));

    return response;
  }
}
