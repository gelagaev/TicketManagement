using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Configurations;

public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
  private readonly IApiVersionDescriptionProvider _provider;

  public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

  /// <summary>
  /// Configure each API discovered for Swagger Documentation
  /// </summary>
  /// <param name="options">Swagger Options</param>
  public void Configure(SwaggerGenOptions options)
  {
    foreach (var description in _provider.ApiVersionDescriptions)
    {
      options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
    }

    options.EnableAnnotations();

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
      BearerFormat = "JWT",
      Name = "JWT Authentication",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.Http,
      Scheme = JwtBearerDefaults.AuthenticationScheme,
      Description = "Insert token",
      Reference = new OpenApiReference
      {
        Id = JwtBearerDefaults.AuthenticationScheme,
        Type = ReferenceType.SecurityScheme
      }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
  }

  /// <summary>
  /// Configure Swagger Options. Inherited from the Interface
  /// </summary>
  /// <param name="name"></param>
  /// <param name="options"></param>
  public void Configure(string name, SwaggerGenOptions options)
  {
    Configure(options);
  }

  /// <summary>
  /// Create information about the version of the API
  /// </summary>
  /// <param name="desc"></param>
  /// <returns>Information about the API</returns>
  private static OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
  {
    return new OpenApiInfo
    {
      Title = Assembly.GetEntryAssembly()?.FullName?.Split(",").FirstOrDefault() ?? "API",
      Version = desc.ApiVersion.ToString(),
      Description = desc.IsDeprecated
        ? "This API version has been deprecated. Please use one of the new APIs available from the explorer."
        : "Actual API version."
    };
  }
}
