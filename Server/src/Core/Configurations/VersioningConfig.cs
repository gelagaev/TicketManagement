using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configurations;

public static class VersioningConfig
{
  public static void AddApiVersioning(this WebApplicationBuilder builder)
  {
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
  }
}
