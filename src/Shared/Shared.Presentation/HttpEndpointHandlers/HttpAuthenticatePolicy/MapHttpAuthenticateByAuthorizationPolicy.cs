using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

namespace Shared.Presentation.HttpEndpointHandlers.HttpAuthenticatePolicy;

public class MapHttpAuthenticateByAuthorizationPolicy : IMapHttpAuthenticatePolicy
{
    private readonly Action<AuthorizationPolicyBuilder> _authorizationPolicy;

    public MapHttpAuthenticateByAuthorizationPolicy(Action<AuthorizationPolicyBuilder> authorizationPolicy)
    {
        _authorizationPolicy = authorizationPolicy;
    }

    public void Apply(IEndpointConventionBuilder endpoint)
    {
        endpoint.RequireAuthorization(_authorizationPolicy);
    }
}