using FluentValidation;
using MediatR;
using Shared.Domain.Base.Results;

namespace Shared.Application.Shared.Behaviors;

/// <summary>
/// Represents the validation behavior middleware.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
internal sealed class ValidationBehaviorResult<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    //private readonly IRequestHandler<TRequest, TResponse> _inner;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The validator for the current request type.</param>
    /// <param name="inner"></param>
    public ValidationBehaviorResult(IEnumerable<IValidator<TRequest>> validators,
        IRequestHandler<TRequest, TResponse> inner)
    {
        _validators = validators;
        //_inner = inner;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count == 0)
        {
            return await next();
        }

        var responseType = typeof(TResponse);

        if (!responseType.IsGenericType)
        {
            return (dynamic)Result.Failure(Domain.Errors.SharedErrors.InvalidArguments);
        }

        var resultType = responseType.GetGenericArguments().FirstOrDefault();

        var methodInfo = typeof(Result).GetMethods().First(_ => _ is { Name: "Failure", IsGenericMethod: true });
        var genericMethod = methodInfo.MakeGenericMethod(resultType!);
        dynamic? result = genericMethod.Invoke(null, new[] { Domain.Errors.SharedErrors.InvalidArguments, (object?)default });
        return result ?? Result.Failure(Domain.Errors.SharedErrors.InvalidArguments);
    }
}