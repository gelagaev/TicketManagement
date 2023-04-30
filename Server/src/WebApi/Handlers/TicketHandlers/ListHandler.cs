using Core.TicketAggregate;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

public class ListHandler : IRequestHandler<TicketListCommand, TicketListResponse>
{
  private readonly IReadRepository<Ticket> _repository;

  public ListHandler(IReadRepository<Ticket> repository) => _repository = repository;

  public async Task<TicketListResponse> Handle(TicketListCommand request, CancellationToken ct)
  {
    var tickets = await _repository.ListAsync(ct);
    var response = new TicketListResponse
    {
      Tickets = tickets
        .Select(ticket => new TicketRecord(ticket.Id, ticket.Subject, ticket.Description))
        .ToList()
    };

    return response;
  }
}
