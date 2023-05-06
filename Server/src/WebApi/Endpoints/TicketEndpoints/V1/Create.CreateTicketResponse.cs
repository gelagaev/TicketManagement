namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class CreateTicketResponse
{
  public CreateTicketResponse(TicketRecord ticket)
  {
    Ticket = ticket;
  }

  public TicketRecord Ticket { get; private set; }
}
