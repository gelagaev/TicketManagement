using Core.UserAggregate;
using Core.UserAggregate.Enums;
using Microsoft.AspNetCore.Identity;

namespace WebApi;

public static class SeedData
{
  public static void Initialize(IServiceProvider serviceProvider)
  {
    PopulateUserRoles(serviceProvider);
    PopulateUsers(serviceProvider);
  }

  private static async void PopulateUsers(IServiceProvider serviceProvider)
  {
    const string adminEmail = "admin@test.test";
    const string managerEmail = "manager@test.test";

    using var scope = serviceProvider.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    if ((await userManager.FindByEmailAsync(adminEmail)) == null)
    {
      var admin = new User
      {
        Id = Guid.Parse("57B803B0-D0A6-4B88-AF1F-C51E442EF419"),
        IsActive = true,
        Email = adminEmail,
        FirstName = "Test",
        LastName = "Admin",
        UserName = adminEmail
      };
      await userManager.CreateAsync(admin, "&&&&&&");
      await userManager.AddToRoleAsync(admin, nameof(Roles.Administrator));
    }

    if ((await userManager.FindByEmailAsync(managerEmail)) == null)
    {
      var manager = new User
      {
        Id = Guid.Parse("CE20CEDA-D36C-4B14-A16B-463B471C6CA9"),
        IsActive = true,
        Email = managerEmail,
        FirstName = "Test",
        LastName = "Manager",
        UserName = managerEmail
      };
      await userManager.CreateAsync(manager, "&&&&&&");
      await userManager.AddToRoleAsync(manager, nameof(Roles.Manager));
    }
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
