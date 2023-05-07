using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class CreateCommentRequest : IRequest<ActionResult<CreateCommentResponse>>
{
  public const string Route = "api/V{version:apiVersion}/Tickets/{TicketId:Guid}/Comments";
  public static string BuildRoute(Guid ticketId) => Route.Replace("{TicketId:Guid}", ticketId.ToString());

  [Required]
  [FromRoute]
  public Guid TicketId { get; set; }

  [Required]
  [FromBody]
  public string CommentText { get; set; } = string.Empty;
}
