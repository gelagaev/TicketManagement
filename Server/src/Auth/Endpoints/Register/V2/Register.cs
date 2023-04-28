using Ardalis.ApiEndpoints;
using Auth.Endpoints.Register.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth.Endpoints.Register.V2;

public class Register : EndpointBaseAsync
  .WithRequest<RegisterRequest>
  .WithActionResult<RegisterResponse>
{
  private readonly IMediator _mediator;

  public Register(IMediator mediator) => _mediator = mediator;

  [ApiVersion("2.0")]
  [HttpPost("api/V{version:apiVersion}/Register")]
  [SwaggerOperation(
    Summary = "Register",
    Description = "Register",
    OperationId = "Auth.Register",
    Tags = new[] { "AuthEndpoints" })
  ]
  public override async Task<ActionResult<RegisterResponse>> HandleAsync(
    RegisterRequest request,
    CancellationToken ct = new())
  {
    var response = await _mediator.Send(request, ct);
    return Ok(response);
  }
}
