using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core;
using Core.Configurations;
using Core.Middleware;
using Core.UserAggregate;
using FluentValidation;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Interfaces;
using WebApi.Providers;
using WebApi.Services;
using WebApi.Validators.TicketValidators;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddIdentity<User, Role>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddRoleManager<RoleManager<Role>>();

builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
  cfg.RegisterServicesFromAssemblyContaining<DefaultCoreModule>();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connectionString!);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(opt =>
{
  opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
  opt.AssumeDefaultVersionWhenUnspecified = true;
  opt.ReportApiVersions = true;
  opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
    new HeaderApiVersionReader("x-api-version"),
    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
  setup.GroupNameFormat = "'V'VVV";
  setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateRequestValidator>(includeInternalTypes: true);

builder.Services.AddControllers();

builder.ConfigureBearerAuth();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterType<ExceptionHandlingMiddleware>();
  containerBuilder.RegisterType<CurrentUserProvider>().As<ICurrentUserProvider>().InstancePerLifetimeScope();
  containerBuilder.RegisterType<TicketPermissionAccessService>().As<ITicketPermissionAccessService>().InstancePerLifetimeScope();
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.IsDevelopment()));
});

var app = builder.Build();

app.UseSwagger();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {ExceptionMessage}", ex.Message);
  }
}

app.Run();
