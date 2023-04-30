﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class CreateCommentRequest
{
  public const string Route = "/Tickets/{TicketId:Guid}/Comments";
  public static string BuildRoute(Guid ticketId) => Route.Replace("{TicketId:Guid}", ticketId.ToString());

  [Required]
  [FromRoute]
  public Guid TicketId { get; set; }

  [Required]
  public string CommentText { get; set; } = string.Empty;
}