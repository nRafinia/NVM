namespace SharedKernel.Base.Commands;

/// <summary>
/// Marker interface to represent a request without a entity
/// </summary>
public interface ICommand : IRequest<Result>;

/// <summary>
/// Marker interface to represent a request with a response entity
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>

public interface ICommand<TResponse> : IRequest<Result<TResponse?>>;