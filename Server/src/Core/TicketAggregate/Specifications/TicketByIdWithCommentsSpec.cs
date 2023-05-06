using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public sealed class TicketByIdWithCommentsSpec : Specification<Ticket>, ISingleResultSpecification
{
  public TicketByIdWithCommentsSpec(Guid ticketId)
  {
    Query
        .Where(ticket => ticket.Id == ticketId)
        .Include(ticket => ticket.Comments)
        .ThenInclude(comment => comment.Author)
        .Include(thicket => thicket.Author);
  }
}
