﻿namespace WebApi.Endpoints.TicketEndpoints.V1;

public record CommentRecord(Guid Id, string CommentText, Guid UserId);
