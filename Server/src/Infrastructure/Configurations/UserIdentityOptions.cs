namespace Infrastructure.Configurations;

public class UserIdentityOptions
{
  public bool RequireUniqueEmail { get; set; }
  public bool RequireDigit { get; set; }
  public int RequiredLength { get; set; }
  public int RequiredUniqueChars { get; set; }
  public bool RequireLowercase { get; set; }
  public bool RequireNonAlphanumeric { get; set; }
  public bool RequireUppercase { get; set; }
}
