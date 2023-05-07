using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class CloseTicketRequest : IRequest<ActionResult>
{
  public const string Route = "api/V{version:apiVersion}/Tickets/{TicketId:Guid}/Close";
  [FromRoute]
  public Guid TicketId { get; set; }
}
