using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class TicketListCommand : IRequest<TicketListResponse>
{
  public const string Route = "api/V{version:apiVersion}/Tickets";
}
