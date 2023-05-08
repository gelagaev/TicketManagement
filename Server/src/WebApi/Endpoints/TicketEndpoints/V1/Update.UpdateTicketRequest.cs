using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public sealed class UpdateTicketRequest : IRequest<UpdateTicketResponse>
{
  public const string Route = "api/V{version:apiVersion}/Tickets/{TicketId:Guid}";
  [Required]
  [FromRoute]
  public Guid Id { get; set; }

  [Required]
  public string Subject { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
}
