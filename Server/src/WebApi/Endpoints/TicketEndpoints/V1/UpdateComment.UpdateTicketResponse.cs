namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class UpdateTicketCommentResponse
{
  public UpdateTicketCommentResponse(CommentRecord comment)
  {
    Comment = comment;
  }
  public CommentRecord Comment { get; private set; }
}
