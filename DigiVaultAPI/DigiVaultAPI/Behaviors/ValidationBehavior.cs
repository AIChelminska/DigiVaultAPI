using FluentValidation;
using MediatR;

namespace DigiVaultAPI.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Jeśli nie ma żadnego validatora dla tego requestu — przejdź dalej
        if (!_validators.Any())
            return await next();

        // Uruchom wszystkie validatory
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        // Jeśli są błędy — rzuć wyjątek zanim handler zostanie wywołany
        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}
