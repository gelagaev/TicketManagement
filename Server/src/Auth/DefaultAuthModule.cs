using System.IdentityModel.Tokens.Jwt;
using Auth.Interfaces;
using Auth.Services;
using Autofac;

namespace Auth;

public sealed class DefaultAuthModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

    builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

    builder.RegisterType<JwtSecurityTokenHandler>().InstancePerLifetimeScope();
  }
}
