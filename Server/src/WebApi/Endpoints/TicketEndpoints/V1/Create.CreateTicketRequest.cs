using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class CreateTicketRequest : IRequest<CreateTicketResponse>
{
  [Required]
  public string Subject { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
}
