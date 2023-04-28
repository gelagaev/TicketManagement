using Ardalis.ApiEndpoints;
using Core.TicketAggregate;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Create : EndpointBaseAsync
  .WithRequest<CreateTicketRequest>
  .WithActionResult<CreateTicketResponse>
{
  private readonly IRepository<Ticket> _repository;

  public Create(IRepository<Ticket> repository) => _repository = repository;

  [HttpPost("/Tickets")]
  [SwaggerOperation(
    Summary = "Creates a new Ticket",
    Description = "Creates a new Ticket",
    OperationId = "Ticket.Create",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<CreateTicketResponse>> HandleAsync(
    CreateTicketRequest request,
    CancellationToken ct = new())
  {
    var newTicket = new Ticket(request.Subject, request.Description, PriorityStatus.Backlog);
    var createdItem = await _repository.AddAsync(newTicket, ct);
    var response = new CreateTicketResponse
    (
      id: createdItem.Id,
      subject: createdItem.Subject,
      description: createdItem.Description
    );

    return Ok(response);
  }
}
