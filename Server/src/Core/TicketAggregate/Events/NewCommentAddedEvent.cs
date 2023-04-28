using Kernel;

namespace Core.TicketAggregate.Events;

public class NewCommentAddedEvent : DomainEventBase
{
  public Comment NewComment { get; set; }
  public Ticket Ticket { get; set; }

  public NewCommentAddedEvent(Ticket ticket, Comment newItem)
  {
    Ticket = ticket;
    NewComment = newItem;
  }
}
