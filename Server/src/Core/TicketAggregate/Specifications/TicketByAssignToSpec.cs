using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public sealed class TicketByAssignToSpec : Specification<Ticket>, ISingleResultSpecification
{
  public TicketByAssignToSpec(Guid userId)
  {
    Query
      .Include(ticket => ticket.Author)
      .Where(ticket => ticket.AssignedId == userId);
  }
}
