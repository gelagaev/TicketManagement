namespace WebApi.Interfaces;

internal interface ITicketPermissionAccessService
{
  Task<bool> CurrentUserCanAddCommentAsync(Guid ticketId, CancellationToken ct);
  Task<bool> CurrentUserCanDeleteTicketAsync(Guid ticketId, CancellationToken ct);
  Task<bool> CurrentUserCanGetTicket(Guid ticketId, CancellationToken ct);
  Task<bool> CurrentUserCanGetAllTickets(CancellationToken ct);
  Task<bool> CurrentUserCanUpdateTicket(Guid ticketId, CancellationToken ct);
}
