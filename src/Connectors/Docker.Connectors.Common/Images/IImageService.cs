using Connectors.Docker.Abstractions;
using Connectors.Docker.Containers;

namespace Connectors.Docker.Images;

public interface IImageService
{
    public Task<IList<Image>> GetImages(IConnector connector, bool all = false);
    public Task<IList<Container>> GetContainers(IConnector connector, bool all = false);
}