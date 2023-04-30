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
    var newTicket = new Ticket(
      request.Subject,
      request.Description,
      PriorityStatus.Backlog,
      _currentUserProvider.GetUserId()
    );
    var createdItem = await _repository.AddAsync(newTicket, ct);
    var response = new CreateTicketResponse
    (
      id: createdItem.Id,
      subject: createdItem.Subject,
      description: createdItem.Description
    );
    return response;
  }
}
