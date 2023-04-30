using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class DeleteTicketRequest : IRequest<ActionResult>
{
  public const string Route = "/Tickets/{TicketId:Guid}";
  public static string BuildRoute(Guid ticketId) => Route.Replace("{TicketId:Guid}", ticketId.ToString());

  public Guid TicketId { get; set; }
}
