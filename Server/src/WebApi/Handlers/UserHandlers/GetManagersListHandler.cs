using Core.UserAggregate;
using Core.UserAggregate.Specifications;
using Kernel.Interfaces;
using MediatR;
using WebApi.Endpoints.UserEndpoints.V1;

namespace WebApi.Handlers.UserHandlers;

public class GetManagersListHandler : IRequestHandler<GetManagersListCommand, GetManagersListResponse>
{
  private readonly IRepository<User> _repository;

  public GetManagersListHandler(IRepository<User> repository) => _repository = repository;

  public async Task<GetManagersListResponse> Handle(GetManagersListCommand request, CancellationToken ct)
  {
    var spec = new UserWithManagerRoleSpec();
    var users = await _repository.ListAsync(spec, ct);

    return new GetManagersListResponse(users.Select(user => user.ToRecord()).ToList());
  }
}
