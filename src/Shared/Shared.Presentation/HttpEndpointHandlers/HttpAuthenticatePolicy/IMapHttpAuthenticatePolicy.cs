using Microsoft.AspNetCore.Builder;

namespace Shared.Presentation.HttpEndpointHandlers.HttpAuthenticatePolicy;

public interface IMapHttpAuthenticatePolicy
{
    public void Apply(IEndpointConventionBuilder endpoint);
}