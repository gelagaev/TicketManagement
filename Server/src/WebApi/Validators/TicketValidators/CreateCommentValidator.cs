using Core;
using FluentValidation;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Validators.TicketValidators;

internal sealed class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
{
  public CreateCommentValidator(ITicketPermissionAccessService ticketPermissionAccessService)
  {
    RuleFor(request => request.CommentText)
      .NotEmpty()
      .MaximumLength(1000);

    RuleFor(request => request.TicketId)
      .NotEmpty()
      .MustAsync(async (ticketId, ct) => await ticketPermissionAccessService.CurrentUserCanAddCommentAsync(ticketId, ct))
      .WithMessage(nameof(ErrorCodes.ADD_COMMENT_ACCESS_DENIED));
  }
}
