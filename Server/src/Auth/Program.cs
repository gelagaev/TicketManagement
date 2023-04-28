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
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddApiVersioning(opt =>
{
  opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(2, 0);
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

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssemblyContaining<SignInCommandValidator>(includeInternalTypes: true);

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultAuthModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.IsDevelopment()));
});

builder.ConfigureBearerAuth();

var app = builder.Build();

app.UseSwagger();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
