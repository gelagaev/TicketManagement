using Kernel;

namespace Core.TicketAggregate;

public class Comment : EntityBase<Guid>
{
  public Guid UserId { get; private set; }
  public string CommentText { get; private set; }

  public Comment(Guid userId, string commentText)
  {
    UserId = userId;
    CommentText = commentText;
  }

  public override string ToString()
  {
    return $"{Id}: Comment: {CommentText}";
  }
}
