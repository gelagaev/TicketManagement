namespace WebApi.Endpoints.TicketEndpoints.V1;

public record TicketRecord(
  Guid Id,
  Guid AuthorId,
  Guid? AssignId,
  string Subject,
  string Description,
  bool IsDone,
  DateTime CreatedDateTime,
  string? AssignToFullName);
