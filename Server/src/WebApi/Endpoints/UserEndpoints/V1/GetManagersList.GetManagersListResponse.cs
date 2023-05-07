using Core.UserAggregate;

namespace WebApi.Endpoints.UserEndpoints.V1;

public class GetManagersListResponse
{
  public GetManagersListResponse(List<UserRecord> users)
  {
    Users = users;
  }
  public List<UserRecord> Users { get; private set; }
}

public record UserRecord(Guid UserId, string FullName);

public static class UserRecordExtensions
{
  public static UserRecord ToRecord(this User user)
  {
    return new UserRecord(user.Id, user.FullName);
  }
}
