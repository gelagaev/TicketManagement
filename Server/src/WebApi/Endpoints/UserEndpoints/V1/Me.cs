using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.UserEndpoints.V1;

public class Me : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<MeResponse>
{
  private readonly IMediator _mediator;

  public Me(IMediator mediator) => _mediator = mediator;

  [HttpGet(MeCommand.Route)]
  [ApiVersion("1.0")]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets User info",
    Description = "Gets User info",
    OperationId = "User.Me",
    Tags = new[] { "UserEndpoints" })
  ]
  public override async Task<ActionResult<MeResponse>> HandleAsync(CancellationToken ct = new())
  {
    return await _mediator.Send(new MeCommand(), ct);
  }
}

public class MeCommand : IRequest<ActionResult<MeResponse>>
{
  public const string Route = "api/V{version:apiVersion}/Users/Me";
}
