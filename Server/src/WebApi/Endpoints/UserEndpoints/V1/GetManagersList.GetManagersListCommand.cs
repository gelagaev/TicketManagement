using MediatR;

namespace WebApi.Endpoints.UserEndpoints.V1;

public class GetManagersListCommand : IRequest<GetManagersListResponse>
{
  public const string Route = "api/V{version:apiVersion}/Users/Managers";
}
