namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class GetCommentsResponse
{
  public IList<CommentRecord> Comments { get; set; } = default!;
}
