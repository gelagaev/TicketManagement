using System.Reflection;
using Auth;
using Auth.Middleware;
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

const string appTitle = "Tickets Auth API V1";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddIdentityCore<User>();

builder.Services.AddIdentity<User, Role>(cfg =>
  {
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequiredLength = 6;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
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
