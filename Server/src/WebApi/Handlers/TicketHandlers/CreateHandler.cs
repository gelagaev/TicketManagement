using Core.TicketAggregate;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class CreateHandler : IRequestHandler<CreateTicketRequest, CreateTicketResponse>
{
  private readonly IRepository<Ticket> _repository;
  private readonly ICurrentUserProvider _currentUserProvider;

  public CreateHandler(IRepository<Ticket> repository, ICurrentUserProvider currentUserProvider)
  {
    _repository = repository;
    _currentUserProvider = currentUserProvider;
  }

  public async Task<CreateTicketResponse> Handle(CreateTicketRequest request, CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    var newTicket = new Ticket(
      request.Subject,
      request.Description,
      PriorityStatus.Backlog
    );
    newTicket.SetAuthor(user);
    var createdItem = await _repository.AddAsync(newTicket, ct);
    var response = new CreateTicketResponse(createdItem.ToRecord());
    return response;
  }
}
