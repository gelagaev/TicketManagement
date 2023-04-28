using System.ComponentModel.DataAnnotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class UpdateTicketRequest
{
  public const string Route = "/Tickets";
  [Required]
  public Guid Id { get; set; }

  [Required] public string Subject { get; set; } = string.Empty;
  [Required] public string Description { get; set; } = string.Empty;
}
