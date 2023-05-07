using System.Reflection;
using Auth;
using Auth.Validators;
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

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.AddIdentity();

builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
  cfg.RegisterServicesFromAssemblyContaining<DefaultCoreModule>();
});

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options
  .UseSqlServer(connectionString));

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

builder.Services.AddValidatorsFromAssemblyContaining<SignInRequestValidator>(includeInternalTypes: true);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultAuthModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.IsDevelopment()));
});

builder.ConfigureBearerAuth();

var app = builder.Build();

app.UseCors();

app.UseSwagger();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
