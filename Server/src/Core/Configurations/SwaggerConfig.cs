using FastEndpoints.Swagger.Swashbuckle;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core.Configurations;

/// <summary>
/// Swagger configuration
/// </summary>
public static class SwaggerConfig
{
  /// <summary>
  /// Register swagger middlewares
  /// </summary>
  /// <param name="app">Application</param>
  /// <param name="name">The description that appears in the document selector drop-down</param>
  public static void UseAppSwagger(this WebApplication app, string name)
  {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", name));
  }

  /// <summary>
  /// Configure swagger
  /// </summary>
  /// <param name="services">ServiceCollection</param>
  /// <param name="title">Swagger UI title</param>
  public static void ConfigureSwagger(this IServiceCollection services, string title)
  {
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });
      c.EnableAnnotations();
      c.OperationFilter<FastEndpointsOperationFilter>();
      var jwtSecurityScheme = new OpenApiSecurityScheme
      {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Введите токен",
        Reference = new OpenApiReference
        {
          Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme
        }
      };

      c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

      c.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
    });
  }
}
