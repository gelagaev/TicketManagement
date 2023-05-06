using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public sealed class TicketByUserIdSpec : Specification<Ticket>, ISingleResultSpecification
{
  public TicketByUserIdSpec(Guid userId)
  {
    Query
      .Include(ticket => ticket.Author)
      .Where(ticket => ticket.Author.Id == userId);
  }
}
