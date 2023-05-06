namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class CreateCommentResponse
{
  public CreateCommentResponse(CommentRecord comment)
  {
    Comment = comment;
  }

  public CommentRecord Comment { get; private set; }
}
