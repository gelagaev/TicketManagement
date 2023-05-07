using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Endpoints.UserEndpoints.V1;
using WebApi.Interfaces;

namespace WebApi.Handlers.UserHandlers;

internal sealed class MeHandler : IRequestHandler<MeCommand, ActionResult<MeResponse>>
{
  private readonly ICurrentUserProvider _currentUserProvider;

  public MeHandler(ICurrentUserProvider currentUserProvider) => _currentUserProvider = currentUserProvider;

  public async Task<ActionResult<MeResponse>> Handle(MeCommand request, CancellationToken ct)
  {
    var user = await _currentUserProvider.GetUserAsync();
    var userInfo = new MeResponse
    {
      Id = user.Id,
      FullName = user.FullName,
      IsAdministrator = user.IsAdministrator,
      IsClient = user.IsClient,
      IsManager = user.IsManager
    };
    return userInfo;
  }
}
