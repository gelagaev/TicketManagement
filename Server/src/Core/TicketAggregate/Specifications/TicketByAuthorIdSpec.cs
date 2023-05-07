using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public sealed class TicketByAuthorIdSpec : Specification<Ticket>, ISingleResultSpecification
{
  public TicketByAuthorIdSpec(Guid authorId)
  {
    Query
      .Include(comment => comment.Author)
      .Include(ticket => ticket.AssignedTo)
      .Where(comment => comment.Author.Id == authorId);
  }
}
