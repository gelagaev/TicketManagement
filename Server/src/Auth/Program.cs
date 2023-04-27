using System.Reflection;
using Auth;
using Auth.Middleware;
using Auth.Options;
using Auth.Validators;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Configurations;
using Core.UserAggregate;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

const string appTitle = "Tickets Management Auth API V1";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options
  .UseSqlServer(connectionString)
  .UseLazyLoadingProxies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssemblyContaining<SignInCommandValidator>(includeInternalTypes: true);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultAuthModule());
  containerBuilder.RegisterModule(
    new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

builder.Services.ConfigureSwagger(appTitle);
builder.ConfigureBearerAuth();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseAppSwagger(appTitle);
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
