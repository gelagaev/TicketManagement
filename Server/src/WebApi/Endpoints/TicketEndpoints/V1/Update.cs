using Ardalis.ApiEndpoints;
using Core.TicketAggregate;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class Update : EndpointBaseAsync
    .WithRequest<UpdateTicketRequest>
    .WithActionResult<UpdateTicketResponse>
{
  private readonly IRepository<Ticket> _repository;

  public Update(IRepository<Ticket> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdateTicketRequest.Route)]
  [SwaggerOperation(
      Summary = "Updates a Ticket",
      Description = "Updates a Ticket. Only supports changing the subject and description.",
      OperationId = "Tickets.Update",
      Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<UpdateTicketResponse>> HandleAsync(
    UpdateTicketRequest request,
      CancellationToken ct = new ())
  {
    var existingTicket = await _repository.GetByIdAsync(request.Id, ct);
    if (existingTicket == null)
    {
      return NotFound();
    }

    existingTicket.UpdateSubject(request.Subject);
    existingTicket.UpdateDescription(request.Description);

    await _repository.UpdateAsync(existingTicket, ct);

    var response = new UpdateTicketResponse(
        ticket: new TicketRecord(existingTicket.Id, existingTicket.Subject, existingTicket.Description)
    );

    return Ok(response);
  }
}
