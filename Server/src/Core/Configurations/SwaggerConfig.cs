using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
  public static void UseSwagger(this WebApplication app)
  {
    if (app.Environment.IsProduction() )
    {
      return;
    }

    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    SwaggerBuilderExtensions.UseSwagger(app);
    app.UseSwaggerUI(c =>
    {
      foreach (var description in provider.ApiVersionDescriptions.Reverse())
      {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
      }
    });
  }
}
