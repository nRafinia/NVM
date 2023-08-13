using Microsoft.AspNetCore.Builder;

namespace Shared.Presentation.HttpEndpointHandlers.HttpAuthenticatePolicy;

public class MapHttpAuthenticateByPolicyName : IMapHttpAuthenticatePolicy
{
    private readonly string[] _policyNames;

    public MapHttpAuthenticateByPolicyName(params string[] policyNames)
    {
        _policyNames = policyNames;
    }

    public void Apply(IEndpointConventionBuilder endpoint)
    {
        endpoint.RequireAuthorization(_policyNames);
    }
}