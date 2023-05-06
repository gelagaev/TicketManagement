using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Middleware;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
  private readonly ILogger<ExceptionHandlingMiddleware> _logger;

  public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (ValidationException e)
    {
      await HandleExceptionAsync(context, e);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "{EMessage}", e.Message);
      throw;
    }
  }

  private static Task HandleExceptionAsync(HttpContext httpContext, ValidationException exception)
  {
    var response = new
    {
      status = StatusCodes.Status422UnprocessableEntity,
      detail = exception.Message,
      errors = exception.Errors,
    };

    httpContext.Response.ContentType = "application/json";

    httpContext.Response.StatusCode = response.status;

    return httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
  }
}
