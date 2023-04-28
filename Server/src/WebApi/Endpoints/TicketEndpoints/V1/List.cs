using Ardalis.ApiEndpoints;
using Core.TicketAggregate;
using Kernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.TicketEndpoints.V1;

public class List : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<TicketListResponse>
{
  private readonly IReadRepository<Ticket> _repository;

  public List(IReadRepository<Ticket> repository) => _repository = repository;

  [HttpGet("/Tickets")]
  [SwaggerOperation(
    Summary = "Gets a list of all Tickets",
    Description = "Gets a list of all Tickets",
    OperationId = "Ticket.List",
    Tags = new[] { "TicketEndpoints" })
  ]
  public override async Task<ActionResult<TicketListResponse>> HandleAsync(CancellationToken ct = new())
  {
    var tickets = await _repository.ListAsync(ct);
    var response = new TicketListResponse
    {
      Tickets = tickets
        .Select(ticket => new TicketRecord(ticket.Id, ticket.Subject, ticket.Description))
        .ToList()
    };

    return Ok(response);
  }
}
