using Connectors.Docker.Containers;
using Connectors.Docker.Images;

namespace Connectors.Docker.Abstractions;

public interface IConnector
{
    Task<IList<Image>> GetImages(bool all = false);
    Task<IList<Container>> GetContainers(bool all = false);
}