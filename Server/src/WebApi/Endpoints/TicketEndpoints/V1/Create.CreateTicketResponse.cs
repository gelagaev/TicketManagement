namespace WebApi.Endpoints.TicketEndpoints.V1;

public class CreateTicketResponse
{
  public CreateTicketResponse(Guid id, string subject, string description)
  {
    Id = id;
    Subject = subject;
    Description = description;
  }
  public Guid Id { get; set; }
  public string Subject { get; set; }
  public string Description { get; set; }
}
