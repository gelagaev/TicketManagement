using System.IdentityModel.Tokens.Jwt;
using Auth.Interfaces;
using Auth.Middleware;
using Auth.Services;
using Auth.Services.EmailIsTaken;
using Autofac;

namespace Auth;

public sealed class DefaultAuthModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

    builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

    builder.RegisterType<EmailIsTakenProvider>().As<IEmailIsTakenProvider>().InstancePerLifetimeScope();

    builder.RegisterType<JwtSecurityTokenHandler>().InstancePerLifetimeScope();

    builder.RegisterType<ExceptionHandlingMiddleware>();
  }
}
