using Connectors.Docker.Abstractions;
using Connectors.Docker.Containers;
using Microsoft.Extensions.Logging;

namespace Connectors.Docker.Images;

public class ImageService : IImageService
{
    private readonly ILogger<ImageService> _logger;

    public ImageService(ILogger<ImageService> logger)
    {
        _logger = logger;
    }

    public Task<IList<Image>> GetImages(IConnector connector, bool all = false)
    {
        try
        {
            return connector.GetImages(all);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get images, exception={Exception}", e.Message);
            throw;
        }
    }

    public Task<IList<Container>> GetContainers(IConnector connector, bool all = false)
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