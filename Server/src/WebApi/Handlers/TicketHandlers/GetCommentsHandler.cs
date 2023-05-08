using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

public class GetCommentsHandler : IRequestHandler<GetCommentsRequest, GetCommentsResponse>
{
  private readonly IReadRepository<Ticket> _repository;

  public GetCommentsHandler(IReadRepository<Ticket> repository) => _repository = repository;

  public async Task<GetCommentsResponse> Handle(GetCommentsRequest request, CancellationToken ct)
  {
    var spec = new TicketByIdWithCommentsSpec(request.TicketId);
    var ticket = await _repository.FirstOrDefaultAsync(spec, ct);
    if (ticket == null) return new();

    return new GetCommentsResponse
    {
      Comments = ticket.Comments
        .Select(comment => comment.ToRecord(ticket.Id))
        .ToList()
    };
  }
}
