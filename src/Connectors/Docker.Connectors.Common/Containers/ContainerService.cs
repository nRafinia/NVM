using Connectors.Docker.Abstractions;
using Microsoft.Extensions.Logging;

namespace Connectors.Docker.Containers;

public class ContainerService : IContainerService
{
    private readonly ILogger<ContainerService> _logger;

    public ContainerService(ILogger<ContainerService> logger)
    {
        _logger = logger;
    }

    public Task<IList<Container>> GetList(IConnector connector, bool all = false)
    {
        try
        {
            return connector.GetContainers(all);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get containers, exception={Exception}", e.Message);
            throw;
        }
    }    

}