
namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class TicketListResponse
{
  public List<TicketRecord> Tickets { get; set; } = new();
}
