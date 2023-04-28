namespace WebApi.Endpoints.TicketEndpoints.V1;

public class UpdateTicketResponse
{
  public UpdateTicketResponse(TicketRecord ticket)
  {
    Ticket = ticket;
  }
  public TicketRecord Ticket { get; set; }
}
