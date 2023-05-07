using Ardalis.GuardClauses;
using Core.TicketAggregate;
using Core.TicketAggregate.Specifications;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class GetByIdHandler : IRequestHandler<GetTicketByIdRequest, GetTicketByIdResponse>
{
  private readonly IRepository<Ticket> _repository;

  public GetByIdHandler(IRepository<Ticket> repository) => _repository = repository;
  public async Task<GetTicketByIdResponse> Handle(GetTicketByIdRequest request, CancellationToken ct)
  {
    var spec = new TicketByIdWithCommentsSpec(request.TicketId);
    var entity = await _repository.FirstOrDefaultAsync(spec, ct);
    Guard.Against.Null(entity, nameof(entity));

    var response = new GetTicketByIdResponse
    (
      id: entity.Id,
      subject: entity.Subject,
      description: entity.Description,
      comments: entity.Comments.Select(item => item.ToRecord()).ToList()
    );

    return response;
  }
}
