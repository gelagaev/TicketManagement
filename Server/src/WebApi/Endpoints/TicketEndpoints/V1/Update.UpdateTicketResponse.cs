namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class UpdateTicketResponse
{
  public UpdateTicketResponse(TicketRecord ticket)
  {
    Ticket = ticket;
  }
  public TicketRecord Ticket { get; private set; }
}
