namespace Dashboard.Domain.Base.Queries;


/// <summary>
/// Defines a handler for a request
/// </summary>
/// <typeparam name="TRequest">The type of request being handled</typeparam>
/// <typeparam name="TResponse">The type of response from the handler</typeparam>
public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse?>>
    where TRequest : IQuery<TResponse>;