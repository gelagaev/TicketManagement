namespace WebApi.Endpoints.TicketEndpoints.V1;

public record CommentRecord(Guid Id,
  Guid TicketId,
  string CommentText,
  Guid AuthorId,
  string AuthorFullName,
  DateTime CreatedDateTime);
