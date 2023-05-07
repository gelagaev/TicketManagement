namespace WebApi.Endpoints.TicketEndpoints.V1;

public record TicketRecord(Guid Id, string Subject, string Description, bool IsDone, DateTime CreatedDateTime);
