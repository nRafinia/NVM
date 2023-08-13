using MediatR;
using Microsoft.AspNetCore.Http;

namespace Shared.Presentation.HttpEndpointHandlers;

public interface IHttpRequest : IRequest<IResult>
{
}