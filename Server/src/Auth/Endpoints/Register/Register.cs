using Ardalis.ApiEndpoints;
using Auth.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth.Endpoints.Register;

public class Register : EndpointBaseAsync
  .WithRequest<RegisterRequest>
  .WithActionResult<RegisterResponse>
{
  private readonly IMediator _mediator;

  public Register(IMediator mediator) =>_mediator = mediator;

  [HttpPost(RegisterRequest.Route)]
  [SwaggerOperation(
    Summary = "Register",
    Description = "Register",
    OperationId = "Auth.Register",
    Tags = new[] { "AuthEndpoints" })
  ]
  public override async Task<ActionResult<RegisterResponse>> HandleAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = new())
  {
    var response = await _mediator.Send(request.ToCommand(), cancellationToken);
    return Ok(response);
  }
}
