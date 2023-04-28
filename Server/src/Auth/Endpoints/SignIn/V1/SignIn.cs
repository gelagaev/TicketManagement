using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth.Endpoints.SignIn.V1;

public class Create : EndpointBaseAsync
  .WithRequest<SignInRequest>
  .WithActionResult<SignInResponse>
{
  private readonly IMediator _mediator;

  public Create(IMediator mediator) => _mediator = mediator;

  [ApiVersion("1.0")]
  [HttpPost("api/V{version:apiVersion}/SignIn")]
  [SwaggerOperation(
    Summary = "SignIn",
    Description = "SignIn",
    OperationId = "Auth.SignIn",
    Tags = new[] { "AuthEndpoints" })
  ]
  public override async Task<ActionResult<SignInResponse>> HandleAsync(
    SignInRequest request,
    CancellationToken ct = new())
  {
    var response = await _mediator.Send(request, ct);
    return Ok(response);
  }
}
