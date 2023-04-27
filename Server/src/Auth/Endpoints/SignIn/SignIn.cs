using Ardalis.ApiEndpoints;
using Auth.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth.Endpoints.SignIn;

public class Create : EndpointBaseAsync
  .WithRequest<SignInRequest>
  .WithActionResult<SignInResponse>
{
  private readonly IMediator _mediator;

  public Create(IMediator mediator) =>_mediator = mediator;

  [HttpPost(SignInRequest.Route)]
  [SwaggerOperation(
    Summary = "SignIn",
    Description = "SignIn",
    OperationId = "Auth.SignIn",
    Tags = new[] { "AuthEndpoints" })
  ]
  public override async Task<ActionResult<SignInResponse>> HandleAsync(
    SignInRequest request,
    CancellationToken cancellationToken = new())
  {
    var response = await _mediator.Send(request.ToCommand(), cancellationToken);
    return Ok(response);
  }
}
