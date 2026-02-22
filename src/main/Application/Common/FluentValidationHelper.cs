using FluentValidation;
using FluentValidation.Results;
using JacksonVeroneze.NET.Result;

namespace UrlShortener.Application.Common;

public static class FluentValidationHelper
{
    public static (bool HasErrors, Error[] Errors) ValidateRequest<TRequest>(
        TRequest request,
        IEnumerable<IValidator<TRequest>> validators)
    {
        ValidationContext<TRequest> context = new(request);

        IEnumerable<ValidationResult> validationResults =
            validators.Select(validator => validator.Validate(context));

        ValidationFailure[] failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToArray();

        Error[] errors = failures
            .Select(failure => Error.Create(
                failure.ErrorCode,
                failure.ErrorMessage,
                failure.PropertyName.ToLowerInvariant()))
            .ToArray();

        return (errors.Length >= 1, errors);
    }
}
