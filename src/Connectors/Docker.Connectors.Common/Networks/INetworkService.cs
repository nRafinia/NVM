using Connectors.Docker.Abstractions;

namespace Connectors.Docker.Networks;

public interface INetworkService
{
    Task<IList<Network>> GetList(IConnector connector);
}