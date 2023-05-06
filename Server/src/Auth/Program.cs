using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Auth;
using Auth.Options;
using Auth.Validators;
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
using Microsoft.AspNetCore.Mvc;
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

builder.Services.AddApiVersioning(opt =>
{
  opt.DefaultApiVersion = new ApiVersion(2, 0);
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
