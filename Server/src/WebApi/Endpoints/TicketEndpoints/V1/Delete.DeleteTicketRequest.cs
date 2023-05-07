using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class DeleteTicketRequest : IRequest<ActionResult>
{
  public const string Route = "api/V{version:apiVersion}/Tickets/{TicketId:Guid}";
  public static string BuildRoute(Guid ticketId) => Route.Replace("{TicketId:Guid}", ticketId.ToString());

  [FromRoute] public Guid TicketId { get; set; }
}
