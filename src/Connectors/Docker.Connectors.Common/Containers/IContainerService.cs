using Connectors.Docker.Abstractions;

namespace Connectors.Docker.Containers;

public interface IContainerService
{
    public Task<IList<Container>> GetList(IConnector connector, bool all = false);
}