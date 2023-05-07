using Ardalis.GuardClauses;
using Core.UserAggregate;
using Kernel;
using Kernel.Interfaces;

namespace Core.TicketAggregate;

public class Comment : EntityBase<Guid>, IAggregateRoot
{
  public virtual User Author { get; set; } = default!;
  public string CommentText { get; set; } = default!;
  public DateTime CreatedDateTime { get; } = DateTime.UtcNow;

  public void UpdateCommentText(string newCommentText)
  {
    CommentText = Guard.Against.NullOrEmpty(newCommentText, nameof(newCommentText));
  }

  public override string ToString()
  {
    return $"{Id}: Comment: {CommentText}";
  }
}
