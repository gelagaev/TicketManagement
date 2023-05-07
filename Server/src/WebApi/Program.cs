using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core;
using Core.Configurations;
using Core.Middleware;
using FluentValidation;
using Infrastructure;
using Infrastructure.Configurations;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Interfaces;
using WebApi.Providers;
using WebApi.Services;
using WebApi.Validators.TicketValidators;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.AddIdentity();

builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
  cfg.RegisterServicesFromAssemblyContaining<DefaultCoreModule>();
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connectionString!);

builder.Services.AddEndpointsApiExplorer();

builder.AddApiVersioning();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(p => p
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
  );
});

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

app.UseCors();

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
