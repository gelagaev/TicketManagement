using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public sealed class AllTicketsSpec : Specification<Ticket>
{
  public AllTicketsSpec()
  {
    Query
      .Include(ticket => ticket.Author)
      .Include(ticket => ticket.AssignedTo);
  }
}
