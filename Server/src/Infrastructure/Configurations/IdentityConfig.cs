using Core.UserAggregate;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class IdentityConfig
{
  public static void AddIdentity(this WebApplicationBuilder builder)
  {
    var userIdentityOptions = builder.Configuration
      .GetSection(nameof(UserIdentityOptions))
      .Get<UserIdentityOptions>();
    builder.Services.AddIdentity<User, Role>(cfg =>
      {
        cfg.User.RequireUniqueEmail = userIdentityOptions.RequireUniqueEmail;
        cfg.Password.RequireDigit = userIdentityOptions.RequireDigit;
        cfg.Password.RequiredLength = userIdentityOptions.RequiredLength;
        cfg.Password.RequiredUniqueChars = userIdentityOptions.RequiredUniqueChars;
        cfg.Password.RequireLowercase = userIdentityOptions.RequireLowercase;
        cfg.Password.RequireNonAlphanumeric = userIdentityOptions.RequireNonAlphanumeric;
        cfg.Password.RequireUppercase = userIdentityOptions.RequireUppercase;
      })
      .AddEntityFrameworkStores<AppDbContext>()
      .AddUserManager<UserManager<User>>()
      .AddRoleManager<RoleManager<Role>>();
  }
}
