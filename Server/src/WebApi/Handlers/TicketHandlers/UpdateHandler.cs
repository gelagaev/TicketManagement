using Ardalis.GuardClauses;
using Core.TicketAggregate;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.TicketEndpoints.V1;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class UpdateHandler : IRequestHandler<UpdateTicketRequest, UpdateTicketResponse>
{
  private readonly IRepository<Ticket> _repository;

  public UpdateHandler(IRepository<Ticket> repository) => _repository = repository;

  public async Task<UpdateTicketResponse> Handle(UpdateTicketRequest request, CancellationToken ct)
  {
    var existingTicket = await _repository.GetByIdAsync(request.Id, ct);
    Guard.Against.Null(existingTicket, nameof(existingTicket));

    existingTicket.UpdateSubject(request.Subject);
    existingTicket.UpdateDescription(request.Description);

    await _repository.UpdateAsync(existingTicket, ct);

    var response = new UpdateTicketResponse(
      ticket: new TicketRecord(existingTicket.Id, existingTicket.Subject, existingTicket.Description, existingTicket.IsDone)
    );

    return response;
  }
}
