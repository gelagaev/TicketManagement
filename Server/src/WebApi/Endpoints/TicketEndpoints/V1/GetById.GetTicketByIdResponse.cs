namespace WebApi.Endpoints.TicketEndpoints.V1;

public class GetTicketByIdResponse
{
  public GetTicketByIdResponse(Guid id, string subject, string description, List<CommentRecord> comments)
  {
    Id = id;
    Subject = subject;
    Description = description;
    Comments = comments;
  }

  public Guid Id { get; set; }
  public string Subject { get; set; }
  public string Description { get; set; }
  public List<CommentRecord> Comments { get; set; } = new();
}
