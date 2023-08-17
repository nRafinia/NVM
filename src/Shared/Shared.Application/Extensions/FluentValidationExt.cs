using FluentValidation;
using FluentValidation.Results;
using Shared.Domain.Base;
using Shared.Domain.Base.Results;
using Shared.Domain.Errors;

namespace Shared.Application.Extensions;

public static class FluentValidationExt
{
    public static Result<bool> IsValid<TValidator, T>(this T request)
        where T : class
        where TValidator : AbstractValidator<T>
    {
        var validator = Activator.CreateInstance<TValidator>() as AbstractValidator<T>;
        var validateResponse = validator.Validate(request);
        return validateResponse.IsValid
            ? true
            : Result.Failure<bool>(MapValidationFailureToError(validateResponse.Errors));
    }

    public static async Task<Result<bool>> IsValidAsync<TValidator, T>(this T request,
        CancellationToken cancellationToken)
        where T : class
        where TValidator : AbstractValidator<T>
    {
        var validator = Activator.CreateInstance<TValidator>() as AbstractValidator<T>;
        var validateResponse = await validator.ValidateAsync(request, cancellationToken);

        return validateResponse.IsValid
            ? true
            : Result.Failure<bool>(MapValidationFailureToError(validateResponse.Errors));
    }

    private static Error MapValidationFailureToError(IEnumerable<ValidationFailure> validationFailures)
    {
        var errorMessages = validationFailures
            .GroupBy(e => e.PropertyName)
            .ToDictionary(v => v.Key, v => v.Select(x => x.ErrorMessage).ToArray());
        return SharedErrors.ValidationError(errorMessages);
    }
}