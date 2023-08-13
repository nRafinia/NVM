using MediatR;
using Microsoft.AspNetCore.Http;

namespace Shared.Presentation.HttpEndpointHandlers;

public interface IHttpRequestHandler<in TRequest> : IRequestHandler<TRequest, IResult>
    where TRequest : IHttpRequest
{
}