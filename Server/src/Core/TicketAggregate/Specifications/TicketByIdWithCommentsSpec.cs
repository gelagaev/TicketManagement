using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public class TicketByIdWithCommentsSpec : Specification<Ticket>, ISingleResultSpecification
{
  public TicketByIdWithCommentsSpec(Guid ticketId)
  {
    Query
        .Where(ticket => ticket.Id == ticketId)
        .Include(ticket => ticket.Comments);
  }
}
