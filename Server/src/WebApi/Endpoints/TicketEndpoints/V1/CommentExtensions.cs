using Core.TicketAggregate;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public static class CommentExtensions
{
  public static CommentRecord ToRecord(this Comment comment)
  {
    return new CommentRecord(
      comment.Id,
      comment.CommentText,
      comment.Author.Id,
      comment.Author.FullName,
      comment.CreatedDateTime);
  }
}
