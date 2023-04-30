using Core.TicketAggregate;
using Kernel.Interfaces;
using WebApi.Interfaces;

namespace WebApi.Services;

internal sealed class TicketPermissionAccessService : ITicketPermissionAccessService
{
  private readonly ICurrentUserProvider _currentUserProvider;
  private readonly IReadRepository<Ticket> _repository;

  public TicketPermissionAccessService(
    ICurrentUserProvider currentUserProvider,
    IReadRepository<Ticket> repository)
  {
    _currentUserProvider = currentUserProvider;
    _repository = repository;
  }

  public async Task<bool> CurrentUserCanAddCommentAsync(Guid ticketId, CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    var ticket = await _repository.GetByIdAsync(ticketId, ct);
    return ticket != null && (ticket.IsUserAuthor(user) || user.IsAdministratorOrManager);
  }

  public async Task<bool> CurrentUserCanDeleteTicketAsync(Guid ticketId, CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    var ticket = await _repository.GetByIdAsync(ticketId, ct);
    return ticket != null && ticket.IsUserAuthor(user);
  }

  public async Task<bool> CurrentUserCanGetTicket(Guid ticketId, CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    var ticket = await _repository.GetByIdAsync(ticketId, ct);
    return ticket != null && (ticket.IsUserAuthor(user) || ticket.IsAssignToUser(user) || user.IsAdministrator);
  }

  public async Task<bool> CurrentUserCanGetAllTickets(CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    return user.IsAdministrator;
  }

  public async Task<bool> CurrentUserCanUpdateTicket(Guid ticketId, CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    var ticket = await _repository.GetByIdAsync(ticketId, ct);
    return ticket != null && (ticket.IsUserAuthor(user) || user.IsAdministrator);
  }
}
