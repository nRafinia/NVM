namespace SharedKernel.Base.Commands;

/// <summary>
/// Defines a handler for a request with a void (<see cref="Result" />) response.
/// You do not need to register this interface explicitly with a container as it inherits from the base <see cref="ICommand" /> interface.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, Result>
    where TRequest : ICommand;

/// <summary>
/// Defines a handler for a request
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse?>>
    where TRequest : ICommand<TResponse>;