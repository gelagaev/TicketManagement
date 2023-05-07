using Ardalis.Specification;
using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Core.UserAggregate;
using Infrastructure.Data;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class ListHandler : IRequestHandler<TicketListCommand, TicketListResponse>
{
  private readonly ICurrentUserProvider _currentUserProvider;
  private readonly IReadRepository<Ticket> _repository;

  public ListHandler(ICurrentUserProvider currentUserProvider, IReadRepository<Ticket> repository)
  {
    _currentUserProvider = currentUserProvider;
    _repository = repository;
  }

  public async Task<TicketListResponse> Handle(TicketListCommand request, CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    var tickets = await _repository.ListAsync(GetSpec(user), ct);

    var response = new TicketListResponse
    {
      Tickets = tickets
        .Select(ticket => new TicketRecord(ticket.Id, ticket.Subject, ticket.Description, ticket.IsDone))
        .ToList()
    };

    return response;
  }

  private static Specification<Ticket> GetSpec(User user)
  {
    if (user.IsAdministrator)
      return new AllTicketsSpec();
    if (user.IsManager)
      return new TicketByAssignToSpec(user.Id);
    return new TicketByUserIdSpec(user.Id);
  }
}
