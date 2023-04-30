using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class TicketListCommand : IRequest<TicketListResponse>
{
}
