using FluentValidation;
using MediatR;

namespace Auth.Validators;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
  {
    if (!_validators.Any())
    {
      return await next();
    }

    var context = new ValidationContext<TRequest>(request);

    var failures = (await Task.WhenAll(
        _validators.Select(v => v.ValidateAsync(context, ct))))
      .SelectMany(r => r.Errors)
      .Where(f => f != null)
      .ToList();

    if (failures.Any())
    {
      throw new ValidationException(failures);
    }

    return await next();
  }
}
