using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class GetCommentsRequest : IRequest<GetCommentsResponse>
{
  public const string Route = "api/V{version:apiVersion}/Tickets/{TicketId:Guid}/Comments";
  public static string BuildRoute(Guid ticketId) => Route.Replace("{TicketId:Guid}", ticketId.ToString());

  public Guid TicketId { get; set; }
}
