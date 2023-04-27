using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Auth.Endpoints.SingIn;

public class Create : EndpointBaseAsync
  .WithRequest<SignInRequest>
  .WithActionResult<SignInResponse>
{
  private readonly IMediator _mediator;

  public Create(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpPost(SignInRequest.Route)]
  [SwaggerOperation(
    Summary = "SingIn",
    Description = "SingIn",
    OperationId = "Auth.SingIn",
    Tags = new[] { "AuthEndpoints" })
  ]
  public override async Task<ActionResult<SignInResponse>> HandleAsync(
    SignInRequest request,
    CancellationToken cancellationToken = new())
  {
    var response = await _mediator.Send(new SignInCommand { Password = request.Password, Email = request.Email, }, cancellationToken);
    return Ok(response);
  }
}
