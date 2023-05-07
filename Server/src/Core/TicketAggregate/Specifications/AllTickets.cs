using Ardalis.Specification;

namespace Core.TicketAggregate.Specifications;

public sealed class AllTicketsSpec : Specification<Ticket>
{
  public AllTicketsSpec()
  {
    Query
      .Include(comment => comment.Author);
  }
}
