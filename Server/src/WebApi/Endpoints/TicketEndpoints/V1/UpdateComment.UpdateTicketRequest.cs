using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class UpdateTicketCommentRequest : IRequest<UpdateTicketCommentResponse>
{
  public const string Route = "api/V{version:apiVersion}/Tickets/Comments";
  [Required]
  public Guid Id { get; set; }

  [Required]
  public string CommentText { get; set; } = string.Empty;
}
