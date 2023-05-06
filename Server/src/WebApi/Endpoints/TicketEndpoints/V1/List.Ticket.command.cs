using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class TicketListCommand : IRequest<TicketListResponse>
{
}
