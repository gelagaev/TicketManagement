using Core.TicketAggregate;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public static class TicketExtensions
{
  public static TicketRecord ToRecord(this Ticket ticket)
  {
    return new TicketRecord(
      ticket.Id,
      ticket.AuthorId,
      ticket.AssignedId,
      ticket.Subject,
      ticket.Description,
      ticket.IsDone,
      ticket.CreatedDateTime,
      ticket.AssignedTo?.FullName);
  }
}
