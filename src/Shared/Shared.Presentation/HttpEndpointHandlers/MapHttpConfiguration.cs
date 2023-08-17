using Shared.Presentation.HttpEndpointHandlers.HttpAuthenticatePolicy;

namespace Shared.Presentation.HttpEndpointHandlers;

public class MapHttpConfiguration
{
    public bool AllowAnonymous { get; init; }
    public string? Description { get; init; }
    public string? Name { get; init; }
    public string? Summary { get; init; }
    public IMapHttpAuthenticatePolicy? Policy { get; init; }
    
}