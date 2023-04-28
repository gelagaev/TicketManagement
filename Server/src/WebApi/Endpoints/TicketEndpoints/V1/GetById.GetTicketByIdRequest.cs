
namespace WebApi.Endpoints.TicketEndpoints.V1;

public class GetTicketByIdRequest
{
  public const string Route = "/Tickets/{TicketId:Guid}";
  public static string BuildRoute(Guid ticketId) => Route.Replace("{TicketId:Guid}", ticketId.ToString());

  public Guid TicketId { get; set; }
}
