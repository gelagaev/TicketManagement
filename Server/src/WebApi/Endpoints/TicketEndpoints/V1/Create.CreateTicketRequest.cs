using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class CreateTicketRequest : IRequest<CreateTicketResponse>
{
  public const string Route = "api/V{version:apiVersion}/Tickets";

  [Required]
  public string Subject { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
}
