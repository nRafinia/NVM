using MediatR;
using Shared.Domain.Base.Results;

namespace Shared.Domain.Base.Queries;

/// <summary>
/// Marker interface to represent a request with a response entity
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>

public interface IQuery<TResponse> : IRequest<Result<TResponse?>> { }