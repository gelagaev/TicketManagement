namespace Core.Options;

public class AuthTokenOptions
{
  public string Issuer { get; init; } = string.Empty;
  public string Audience { get; init; } = string.Empty;
  public string SecretKey { get; init; } = string.Empty;
  public int LifeTimeHours { get; init; }
}
