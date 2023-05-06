using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class DeleteTicketCommentRequest : IRequest<ActionResult>
{
  public const string Route = "api/V{version:apiVersion}/Tickets/Comments/{CommentId:Guid}";
  public static string BuildRoute(Guid ticketId) => Route.Replace("{CommentId:Guid}", ticketId.ToString());

  public Guid CommentId { get; set; }
}
