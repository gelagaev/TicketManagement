using Ardalis.GuardClauses;
using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Kernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class CreateCommentHandler : IRequestHandler<CreateCommentRequest, ActionResult<CreateCommentResponse>>
{
  private readonly IRepository<Ticket> _repository;
  private readonly IHttpContextAccessor _contextAccessor;
  private readonly ICurrentUserProvider _currentUserProvider;

  public CreateCommentHandler(IRepository<Ticket> repository,
    IHttpContextAccessor contextAccessor,
    ICurrentUserProvider currentUserProvider)
  {
    _repository = repository;
    _contextAccessor = contextAccessor;
    _currentUserProvider = currentUserProvider;
  }

  public async Task<ActionResult<CreateCommentResponse>> Handle(CreateCommentRequest request, CancellationToken ct)
  {
    var spec = new TicketByIdWithCommentsSpec(request.TicketId);
    var ticket = await _repository.FirstOrDefaultAsync(spec, ct);
    Guard.Against.Null(ticket, nameof(ticket));

    var user = await _currentUserProvider.GetUserAsync();
    var newComment = new Comment
    {
      Author = user,
      CommentText = request.CommentText,
    };

    ticket.AddComment(newComment);
    await _repository.UpdateAsync(ticket, ct);

    var response = new CreateCommentResponse(newComment.ToRecord());

    return response;
  }
}
