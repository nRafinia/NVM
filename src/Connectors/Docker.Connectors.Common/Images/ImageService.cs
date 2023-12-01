using Connectors.Docker.Abstractions;
using Microsoft.Extensions.Logging;

namespace Connectors.Docker.Images;

public class ImageService : IImageService
{
    private readonly ILogger<ImageService> _logger;

    public ImageService(ILogger<ImageService> logger)
    {
        _logger = logger;
    }

    public Task<IList<Image>> GetList(IConnector connector, bool all = false)
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
}