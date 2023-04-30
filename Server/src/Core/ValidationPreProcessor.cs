using FluentValidation;
using MediatR.Pipeline;

namespace Core;

public class ValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationPreProcessor(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

  public async Task Process(TRequest request, CancellationToken ct)
  {
    if (!_validators.Any())
    {
      return;
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
  }
}
