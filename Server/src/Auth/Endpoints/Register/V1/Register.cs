using Ardalis.ApiEndpoints;
using Auth.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth.Endpoints.Register.V1;

public class Register : EndpointBaseAsync
  .WithRequest<RegisterRequest>
  .WithActionResult<RegisterResponse>
{
  private readonly IMediator _mediator;

  public Register(IMediator mediator) => _mediator = mediator;

  [ApiVersion("1.0" , Deprecated = true)]
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
    var response = await _mediator.Send(request.ToCommand(), ct);
    return Ok(response);
  }
}
