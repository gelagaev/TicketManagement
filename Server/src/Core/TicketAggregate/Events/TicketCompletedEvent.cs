using Kernel;

namespace Core.TicketAggregate.Events;

public class TicketCompletedEvent : DomainEventBase
{
  public Ticket Ticket { get; set; }

  public TicketCompletedEvent(Ticket ticket)
  {
    Ticket = ticket;
  }
}
