using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class UpdateTicketRequest : IRequest<UpdateTicketResponse>
{
  public const string Route = "api/V{version:apiVersion}/Tickets";
  [Required]
  public Guid Id { get; set; }

  [Required]
  public string Subject { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
}
