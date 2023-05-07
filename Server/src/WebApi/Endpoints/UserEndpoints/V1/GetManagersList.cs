using Ardalis.ApiEndpoints;
using Core.UserAggregate.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Endpoints.UserEndpoints.V1;

public class GetManagersList : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<GetManagersListResponse>
{
  private readonly IMediator _mediator;

  public GetManagersList(IMediator mediator) => _mediator = mediator;

  [HttpGet(GetManagersListCommand.Route)]
  [ApiVersion("1.0")]
  [Authorize(Roles = nameof(Roles.Administrator))]
  [SwaggerOperation(
    Summary = "Gets all Users with Manager role",
    Description = "Gets all Users with Manager role",
    OperationId = "User.ManagerList",
    Tags = new[] { "UserEndpoints" })
  ]
  public override async Task<ActionResult<GetManagersListResponse>> HandleAsync(CancellationToken ct = new())
  {
    var response = await _mediator.Send(new GetManagersListCommand(), ct);
    return Ok(response);
  }
}
