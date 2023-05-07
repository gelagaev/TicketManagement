namespace WebApi.Endpoints.UserEndpoints.V1;

public class MeResponse
{
  public Guid Id { get; set; }
  public string FullName { get; set; } = String.Empty;
  public bool IsAdministrator { get; set; }
  public bool IsManager { get; set; }
  public bool IsClient { get; set; }
}
