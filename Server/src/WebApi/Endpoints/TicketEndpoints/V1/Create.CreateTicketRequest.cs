using System.ComponentModel.DataAnnotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class CreateTicketRequest
{
  [Required]
  public string Subject { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
}
