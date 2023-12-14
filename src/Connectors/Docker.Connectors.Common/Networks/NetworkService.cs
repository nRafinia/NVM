using Connectors.Docker.Abstractions;
using Microsoft.Extensions.Logging;

namespace Connectors.Docker.Networks;

public class NetworkService : INetworkService
{
    private readonly ILogger<NetworkService> _logger;

    public NetworkService(ILogger<NetworkService> logger)
    {
        _logger = logger;
    }

    public Task<IList<Network>> GetList(IConnector connector)
    {
        try
        {
            return connector.GetNetworks();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get networks, exception={Exception}", e.Message);
            throw;
        }
    }
}