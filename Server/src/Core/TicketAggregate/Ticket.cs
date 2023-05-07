using Ardalis.GuardClauses;
using Core.TicketAggregate.Events;
using Core.UserAggregate;
using Kernel;
using Kernel.Interfaces;

namespace Core.TicketAggregate;

public class Ticket : EntityBase<Guid>, IAggregateRoot
{
  public string Subject { get; private set; }
  public string Description { get; private set; }

  public bool IsDone { get; private set; }
  public DateTime CreatedDateTime { get; } = DateTime.UtcNow;
  private readonly List<Comment> _comments = new();
  public IEnumerable<Comment> Comments => _comments.AsReadOnly();
  public User Author { get; private set; } = default!;
  public Guid AuthorId { get; private set; }
  public User? AssignedTo { get; private set; } = default!;
  public Guid? AssignedId { get; private set; }

  public TicketStatus Status => IsDone ? TicketStatus.Complete : TicketStatus.InProgress;

  public PriorityStatus Priority { get; }

  public Ticket(string subject, string description, PriorityStatus priority, Guid authorId)
  {
    Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
    Description = Guard.Against.NullOrEmpty(description, nameof(description));
    Priority = priority;
    AuthorId = authorId;
  }
  
  public void MarkComplete()
  {
    if (!IsDone)
    {
      IsDone = true;

      RegisterDomainEvent(new TicketCompletedEvent(this));
    }
  }

  public void AddComment(Comment newComment)
  {
    Guard.Against.Null(newComment, nameof(newComment));
    _comments.Add(newComment);

    var newCommentAddedEvent = new NewCommentAddedEvent(this, newComment);
    base.RegisterDomainEvent(newCommentAddedEvent);
  }

  public void UpdateSubject(string newSubject)
  {
    Subject = Guard.Against.NullOrEmpty(newSubject, nameof(newSubject));
  }
  
  public void UpdateDescription(string newDescription)
  {
    Description = Guard.Against.NullOrEmpty(newDescription, nameof(newDescription));
  }

  public void AssignToUser(Guid? userId)
  {
    AssignedId = userId;
  }

  public bool IsUserAuthor(User user)
  {
    return user.Id == AuthorId;
  }

  public bool IsAssignTo(User user)
  {
    return user.Id == AssignedId;
  }

  public void Close()
  {
    IsDone = true;
  }
}
