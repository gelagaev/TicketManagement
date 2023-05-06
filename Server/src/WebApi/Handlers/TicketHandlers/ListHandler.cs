using Ardalis.Specification.EntityFrameworkCore;
using Core.TicketAggregate.Specifications;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Endpoints.TicketEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Handlers.TicketHandlers;

internal sealed class ListHandler : IRequestHandler<TicketListCommand, TicketListResponse>
{
  private readonly ICurrentUserProvider _currentUserProvider;
  private readonly AppDbContext _context;

  public ListHandler(ICurrentUserProvider currentUserProvider, AppDbContext context)
  {
    _currentUserProvider = currentUserProvider;
    _context = context;
  }

  public async Task<TicketListResponse> Handle(TicketListCommand request, CancellationToken ct)
  {
    var userId = _currentUserProvider.GetUserId();

    var queryResult = SpecificationEvaluator.Default.GetQuery(
      query: _context.Tickets,
      specification: new TicketByUserIdSpec(userId));

    var response = new TicketListResponse
    {
      Tickets = await queryResult
        .Select(ticket => new TicketRecord(ticket.Id, ticket.Subject, ticket.Description, ticket.IsDone))
        .ToListAsync(ct)
    };

    return response;
  }
}
