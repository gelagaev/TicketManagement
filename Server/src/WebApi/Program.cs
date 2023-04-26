using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core;
using Core.Configurations;
using Core.UserAggregate;
using FastEndpoints;
using FastEndpoints.ApiExplorer;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

const string appTitle = "Tickets Management Web API V1";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddIdentity<User, Role>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddRoleManager<RoleManager<Role>>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connectionString!);

builder.Services.AddControllers();
builder.Services.AddFastEndpoints();
builder.Services.AddFastEndpointsApiExplorer();

builder.ConfigureBearerAuth();
builder.Services.ConfigureSwagger(appTitle);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(
    new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseAppSwagger(appTitle);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
