using Core.UserAggregate;
using Core.UserAggregate.Enums;
using Microsoft.AspNetCore.Identity;

namespace WebApi;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    PopulateUserRoles(serviceProvider);
  }

  private static async void PopulateUserRoles(IServiceProvider serviceProvider)
  {
    using var scope = serviceProvider.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    foreach (string name in Enum.GetNames(typeof(Roles)))
      if (!await roleManager.RoleExistsAsync(name))
        await roleManager.CreateAsync(new Role { Name = name });
  }
}
