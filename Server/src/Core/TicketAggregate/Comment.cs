using Kernel;

namespace Core.TicketAggregate;

public class Comment : EntityBase<Guid>
{
  public string CommentText { get; set; } = string.Empty;
  public Guid UserId { get; set; }

  public override string ToString()
  {
    return $"{Id}: Comment: {CommentText}";
  }
}
