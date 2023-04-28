
namespace WebApi.Endpoints.TicketEndpoints.V1;

public class TicketListResponse
{
  public List<TicketRecord> Tickets { get; set; } = new();
}
